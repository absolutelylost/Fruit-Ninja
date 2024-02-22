using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    private int hitPoints;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.transform.name == "sword")
		{
			hitPoints -= 2;
			Debug.Log("hit with sword");
		}
		if (collision.collider.transform.name == "Shuriken4")
		{
			hitPoints -= 1;
			Debug.Log("hit with star");

		}
		if (hitPoints == 0) Destroy(gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
        switch (gameObject.name)
        {
			case "apple":
				hitPoints = 5;
				break;
			case "avocado":
				hitPoints = 7;
				break;
			case "banana":
				hitPoints = 6;
				break;
			case "cherries":
				hitPoints = 8;
				break;
			case "lemon":
				hitPoints = 5;
				break;
			case "peach":
				hitPoints = 5;
				break;
			case "peanut":
				hitPoints = 6;
				break;
			case "pear":
				hitPoints = 4;
				break;
			case "strawberry":
				hitPoints = 10;
				break;
			case "watermelon":
				hitPoints = 10;
				break;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
