using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
		//lineRenderer = transform.GetComponent<LineRenderer>();
        //Ray ray = new Ray(transform.position, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) ,out RaycastHit hitInfo, 20f))
        //{
        //    Debug.Log("hit: " + hitInfo.collider.name);
        //}
    }

    public void DrawRope()
    {

    }
}
