using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehavior : MonoBehaviour
{
    public bool isThrown;
	private float timeLimit = 7.0f;
	private float currentTime = 0f;
    private float rotationSpeed = 5000f;


	private void OnCollisionEnter(Collision collision)
	{
        isThrown = false;
	}
	// Start is called before the first frame update
	void Start()
    {
        isThrown = false;

	}

    public void StartTimer()
    {
        isThrown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrown)
        {
            //transform.Rotate(0, 1, 0);
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            rb.angularVelocity = new Vector3(0, 2 * rotationSpeed, 0);
			currentTime += Time.deltaTime;

		}

        if(currentTime > timeLimit)
        {
            Destroy(gameObject);
        }
    }
}
