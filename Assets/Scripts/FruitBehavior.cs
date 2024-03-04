using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    private int hitPoints;
	private float moveSpeed;
	private Vector3[] strikePoints;
	private Vector3[] strikePointsNormals;
	private Vector3 movementDirection;

	[SerializeField] private GameObject gameManager;
	public bool isGrabbed = false;

	//actually not used here
	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("hit");
		////strikePoints[0] = collision.contacts[0].point;
		//Debug.Log(collision.contactCount);
		//Debug.Log(collision.collider.transform.name);

		if (collision.collider.transform.name.Contains("Shuriken"))
		{
			hitPoints -= 1;
			//Debug.Log("hit with star");
			//Debug.Log("hit with star. remaining health: " + hitPoints);
			collision.collider.transform.transform.parent = gameObject.transform;
			//Debug.Log(collision.collider.transform.parent.name);

			//collision.collider.transform.position = collision.contacts[0].point;
			collision.collider.GetComponent<StarBehavior>().isThrown = false;
			collision.collider.GetComponent<Rigidbody>().isKinematic = true;

		}
		if (hitPoints <= 0)
		{
			Debug.Log(gameObject.name.Length);
			gameManager.GetComponent<GameManager>().Score += gameObject.name.Length;
			
			Destroy(gameObject);

		}
	}

	// Start is called before the first frame update
	void Start()
    {
		strikePoints = new Vector3[2];
		hitPoints = gameObject.name.Length / 2;

		switch (gameObject.name)
        {
			case "apple":
				moveSpeed = 5.0f;
				break;
			case "avocado":
				moveSpeed = 1.5f;
				break;
			case "banana":
				moveSpeed = 3.0f;
				break;
			case "cherries":
				moveSpeed = 4f;
				break;
			case "lemon":
				moveSpeed = 2.5f;
				break;
			case "peach":
				moveSpeed = 2.5f;
				break;
			case "peanut":
				moveSpeed = 3.0f;
				break;
			case "pear":
				moveSpeed = 2.0f;
				break;
			case "strawberry":
				moveSpeed = 5.0f;
				break;
			case "watermelon":
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
