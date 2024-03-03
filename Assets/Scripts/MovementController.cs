using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
	[SerializeField] private GameObject characterHead;
	[SerializeField] private GameObject movementControllerHand;
	[SerializeField] private CharacterController cc;
	[SerializeField] private Slider healthSlider;
	[HideInInspector] public bool freezeMovement = false;



	public InputActionProperty leftHandGripMoveAction;
	private float moveSpeed = 1.0f;
    private bool activeMovement;
	private Vector3 startingHeadPosition;
	private Vector3 currentHeadPosition;
	private Vector3 originHandPos;
	private Vector3 currentHandPos;
	//sweets heal
	private float cakeHeal = 5.0f;
	private float cookieHeal = 2.0f;
	//fruit damage
	private float avocadoDamage = 5.0f;
	private float lemonDamage = 2.0f;



	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("collided with: " + collision.gameObject.name);
		switch (collision.gameObject.layer)
		{
			//collision with sweets to heal
			case 7:
				if (collision.gameObject.name == "Cake")
					healthSlider.value += cakeHeal;
				if (collision.gameObject.name == "cookie")
					healthSlider.value += cookieHeal;
				Destroy(collision.gameObject);
				break;
			//collision with damage dealer
			case 8:
				if (collision.gameObject.name == "avoPit")
					healthSlider.value += avocadoDamage;
				if (collision.gameObject.name == "cookie")
					healthSlider.value += lemonDamage;
				Destroy(collision.gameObject);
				break;

		}
	}

	// Start is called before the first frame update
	void Start()
    {
		activeMovement = false;
		startingHeadPosition = characterHead.transform.position;
	}

	// Update is called once per frame
	void Update()
    {

        if (freezeMovement)
		{
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}


		float gripValue = leftHandGripMoveAction.action.ReadValue<float>();

		if (gripValue >= 0.90 && !activeMovement)
        {
            activeMovement = true;
			originHandPos = movementControllerHand.transform.position;
			startingHeadPosition = characterHead.transform.position;
		}
		if(gripValue <= 0.10)
		{
			activeMovement = false;
		}
		if (activeMovement)
		{
			//Vector3 forwardDirection = characterHead.transform.TransformDirection(Vector3.forward);
			
			Vector3 newPos = movementControllerHand.transform.position;

			Vector3 forwardDirection = newPos - originHandPos;
			forwardDirection.Normalize();

			cc.SimpleMove(forwardDirection * moveSpeed);
		}
		//Debug.Log(currentHeadPosition);

	}
}
