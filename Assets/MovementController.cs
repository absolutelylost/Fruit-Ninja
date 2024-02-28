using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
	[SerializeField] private GameObject characterHead;
	[SerializeField] private CharacterController cc;

	public InputActionProperty leftHandGripMoveAction;
	private float moveSpeed = 5.0f;
    private bool activeMovement;
	private Vector3 startingHeadPosition;
	private Vector3 currentHeadPosition;


	// Start is called before the first frame update
	void Start()
    {
		activeMovement = false;
		startingHeadPosition = characterHead.transform.position;
		Debug.Log(startingHeadPosition);
	}

	// Update is called once per frame
	void Update()
    {
		float gripValue = leftHandGripMoveAction.action.ReadValue<float>();

		if (gripValue >= 0.90 && !activeMovement)
        {
            activeMovement = true;
			startingHeadPosition = characterHead.transform.position;
		}
		if(gripValue <= 0.10)
		{
			activeMovement = false;
		}
		if (activeMovement)
		{
			Vector3 forwardDirection = characterHead.transform.TransformDirection(Vector3.forward);
			cc.SimpleMove(forwardDirection * moveSpeed);
		}
		//Debug.Log(currentHeadPosition);

	}
}
