using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{

    public float enemySpeed;

	private void Start()
	{

	}
	private void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "Obstacle")
		{
			//Debug.Log("trigger: " + other.gameObject.tag);
			Destroy(gameObject);

		}
		//if (other.gameObject.tag == "Player")
  //      {
  //          Destroy(gameObject);
  //      }
		if (other.gameObject.name.Contains("Sword"))
        {
			//Debug.Log("connected: " + other.gameObject.name);
			gameObject.GetComponent<Rigidbody>().AddForce(-gameObject.transform.up * enemySpeed);
		}




	}
}
