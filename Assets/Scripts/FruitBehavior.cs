using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    private int hitPoints;
	private float moveSpeed;
	private Vector3[] strikePoints;
	private Vector3[] strikePointsNormals;
	private Vector3 movementDirection;

	[SerializeField] private GameObject fruitAttack;

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("hit");
		//strikePoints[0] = collision.contacts[0].point;
		Debug.Log(collision.contactCount);
		Debug.Log(collision.collider.transform.name);
		if(collision.collider.transform.name == "Sword")
		{
			hitPoints -= 2;
			Debug.Log("hit with sword. remaining health: " + hitPoints);
		}
		if (collision.collider.transform.name == "Shuriken4")
		{
			hitPoints -= 1;
			Debug.Log("hit with star");
			Debug.Log("hit with star. remaining health: " + hitPoints);
		}
		if (hitPoints <= 0)
		{
			Debug.Log("destroy object");
			Destroy(gameObject);
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		Debug.Log("leaving object");
		Debug.Log(collision.contactCount);
		if (collision.contactCount >= 1)
		{
			strikePoints[1] = collision.contacts[0].point;

			Debug.DrawLine(strikePoints[0], strikePoints[1], Color.red);

			strikePoints = new Vector3[strikePoints.Length];

		}
	}

	// Start is called before the first frame update
	void Start()
    {
		strikePoints = new Vector3[2];

		switch (gameObject.name)
        {
			case "apple":
				hitPoints = 5;
				moveSpeed = 5.0f;
				break;
			case "avocado":
				hitPoints = 7;
				moveSpeed = 1.5f;
				break;
			case "banana":
				hitPoints = 6;
				moveSpeed = 3.0f;
				break;
			case "cherries":
				hitPoints = 8;
				moveSpeed = 4f;
				break;
			case "lemon":
				hitPoints = 5;
				moveSpeed = 2.5f;
				break;
			case "peach":
				hitPoints = 5;
				moveSpeed = 2.5f;
				break;
			case "peanut":
				hitPoints = 6;
				moveSpeed = 3.0f;
				break;
			case "pear":
				hitPoints = 4;
				moveSpeed = 2.0f;
				break;
			case "strawberry":
				hitPoints = 10;
				moveSpeed = 5.0f;
				break;
			case "watermelon":
				hitPoints = 10;
				moveSpeed = 1.0f;
				break;
		}
	}

	private void DrawLine(Vector3 entryPoint, Vector3 exitPoint)
	{
		Debug.DrawLine(entryPoint, exitPoint);
	}

    // Update is called once per frame
    void Update()
    {
		//if((int)Time.realtimeSinceStartup % 2 == 0)
		//{
		//	movementDirection = Vector3.down;
		//}
		//else
		//{
		//	movementDirection = Vector3.up;
		//}
		//transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

	}
}
