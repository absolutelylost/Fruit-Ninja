using System;
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

	[SerializeField] private GameObject gameoverScreen;
	[SerializeField] private GameObject XRRig;

	public InputActionProperty leftHandGripMoveAction;
	public InputActionProperty leftPinchAnimationAction;

	public InputActionProperty yButtonPress;
	public InputActionProperty xButtonPress;

	private float moveSpeed = 3.0f;
    private bool activeMovement;
	private Vector3 startingHeadPosition;
	private Vector3 currentHeadPosition;
	private Vector3 originHandPos;
	private Vector3 currentHandPos;
	//sweets heal
	private float cakeHeal = 7.0f;
	private float cookieHeal = 4.0f;
	//fruit damage
	private float avocadoDamage = 5.0f;
	private float lemonDamage = 2.0f;
	private float flyTime = 0.0f;
	private float maxFlyTime = 1.5f;
	private bool flying;
	private bool falling;

	private Vector3 startingPosition;
	private float flySpeed = 10.0f;
	private float upwardForce = 5.0f;


	private void OnTriggerEnter(Collider collision)
	{
		//Debug.Log(collision.gameObject.name.Contains("cookie"));
		
		Debug.Log("collided with: " + collision.gameObject.name);
		switch (collision.gameObject.layer)
		{
			//collision with sweets to heal
			case 7:
				if (collision.gameObject.name.Contains("cake") || collision.gameObject.name.Contains("Cake"))
					healthSlider.value += cakeHeal;
				if (collision.gameObject.name.Contains("cookie") || collision.gameObject.name.Contains("Cookie"))
					healthSlider.value += cookieHeal;
				Destroy(collision.gameObject);
				break;
			//collision with damage dealer
			case 8:
				if (collision.gameObject.name.Contains("AvocadoPitBullet"))
					healthSlider.value -= avocadoDamage;
				if (collision.gameObject.name.Contains("lemonBullet"))
					healthSlider.value -= lemonDamage;
				Destroy(collision.gameObject);
				break;

		}
		//Debug.Log("new health: " + healthSlider.value);
		if(healthSlider.value <= 0)
		{
			gameoverScreen.SetActive(true);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		activeMovement = false;
		startingHeadPosition = characterHead.transform.position;
		startingPosition = transform.position;

	}

	// Update is called once per frame
	void Update()
    {

        if (freezeMovement)
		{
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}


		float gripValue = leftHandGripMoveAction.action.ReadValue<float>();
		bool xIsPressed = xButtonPress.action.IsPressed();
		bool yIsPressed = yButtonPress.action.IsPressed();

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
			Vector3 forwardDirection = characterHead.transform.TransformDirection(Vector3.forward);
			
			//Vector3 newPos = movementControllerHand.transform.position;
			//Vector3 forwardDirection = newPos - originHandPos;
			forwardDirection.Normalize();

			cc.SimpleMove(forwardDirection * moveSpeed);
		}

		if (gripValue >= .90f && xIsPressed || yIsPressed)
		{
			flying = true;

		}

		if (flying)
		{
			flyTime += Time.deltaTime;
			// Calculate the direction at a 45-degree angle
			Vector3 flyDirection = Quaternion.Euler(-45, 0, 0) * transform.forward;

			// flight for forward movement
			transform.position += flyDirection * flySpeed * Time.deltaTime;

			// flight for upward movement
			transform.position += Vector3.up * upwardForce * Time.deltaTime;

		}

		if(flyTime > maxFlyTime)
		{
			Debug.Log("stop flying");
			flying = false;
			flyTime = 0;
			falling = true;

		}

		if (falling)
		{
			Debug.Log(" falling");

			transform.position -= Vector3.up * flySpeed * Time.deltaTime;
			if (transform.position.y <= startingPosition.y)
			{
				falling = false;
			}

		}


	}
}
