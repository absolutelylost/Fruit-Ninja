using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class Slicer : MonoBehaviour
{
    public Transform planeDebug;
    [SerializeField] private Transform startSlicedPoint;
    [SerializeField] public Transform endSlicedPoint;
    public LayerMask sliceableLayer;

	//// objects being sliced
	//public GameObject target;
    public List<Material> crossCutMaterials;
    private float cutForce = 1000f;
	private Vector3 averageVelocity;
	private Vector3 previousPosition;
    private float previousTime = 0;



	// Start is called before the first frame update
	void Start()
    {
        Material[] cutMaterials = Resources.LoadAll<Material>("CutMaterials");
        crossCutMaterials.AddRange(cutMaterials);
	}

	private void Update()
	{
        float currentTime = Time.time;
        float timeChange = currentTime - previousTime;
		averageVelocity = (endSlicedPoint.position - previousPosition) / timeChange;
        previousPosition = endSlicedPoint.position;
        previousTime = currentTime;

	}

	void FixedUpdate()
    {
        //if(Keyboard.current.spaceKey.wasPressedThisFrame) { SliceAction(target); }
        bool isHit = Physics.Linecast(startSlicedPoint.position, endSlicedPoint.position, out RaycastHit hit, sliceableLayer);
        Debug.Log(isHit);
        if (isHit)
        {
            GameObject target = hit.transform.gameObject;
            Debug.Log(target);
            SliceAction(target);
        }

    }

    // handle splitting object
    public void SliceAction(GameObject target)
    {
		// plane is based in this case on the velocity of the sword in a certain direction to find movement direction
		// use start to end position vector. find orientation by getting cross product t get perpendicular
		//Vector3 velocity = endSlicedPoint.GetComponent<Rigidbody>().velocity;
        Debug.Log(averageVelocity);
        Vector3 normal = Vector3.Cross(endSlicedPoint.position - startSlicedPoint.position, averageVelocity);
        normal.Normalize();

		//normal was planeDebug.up
		SlicedHull hull = target.Slice(endSlicedPoint.position, normal);
        // find material based on name of object cut
        Material cutMaterial = crossCutMaterials.Find(x => x.name == target.name);

        //create sections and assign components
		if (hull != null)
        {
            GameObject upperhull = hull.CreateUpperHull(target, cutMaterial);
            SetupSlicedComponent(upperhull);

			GameObject lowerhull = hull.CreateLowerHull(target, cutMaterial);
            SetupSlicedComponent(lowerhull);
            Destroy(target);

		}
	}

    public void SetupSlicedComponent(GameObject postSlicedObject)
    {
        Rigidbody rb = postSlicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = postSlicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        // explosion force, position and radius
        rb.AddExplosionForce(cutForce, postSlicedObject.transform.position, 1);

	}

}
