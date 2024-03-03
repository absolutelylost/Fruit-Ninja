using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
public class AnimateHandOnInput : MonoBehaviour
{

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;
	[SerializeField] private GameObject sword;
	[SerializeField] private GameObject ninjaStar;
	[SerializeField] private TextMeshProUGUI StarValue;

	private Vector3 gripPoint;
	private bool isGrabbed = false;
	private GameObject instantiatedPrefab;
	private float throwForce = 5.0f;
	private Vector3 throwingVelocity;
	public int numThrowingStars;
	public Vector3 velocity;
	private Vector3 currentHandPosition;
	private Vector3 previousHandPosition;


	// Start is called before the first frame update
	void Start()
    {
		isGrabbed = false;
		StarValue.text = numThrowingStars.ToString();
		previousHandPosition = transform.parent.position;
	}

	// Update is called once per frame
	void Update()
    {
		currentHandPosition = transform.parent.position;
		velocity = (currentHandPosition - previousHandPosition) / Time.deltaTime;
		velocity.Normalize();
		//Debug.Log(velocity);
		//right as bool for push or not
		float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
		// both buttons need to be selected
		handAnimator.SetFloat("Grip", gripValue);

		// active sword grip
		if (gripValue > 0.9f)
		{
			if (sword == null) return;
			sword.SetActive(true);
		}
		else
		{
			if (sword == null) return;
			sword.SetActive(false);
		}

		//active ninja star pinch
		if (!isGrabbed && triggerValue > 0.9f && gripValue < 0.1f && ninjaStar != null)
		{
			Debug.Log("star");
			if (ninjaStar == null) return;
			instantiatedPrefab = Instantiate(ninjaStar, transform.position, Quaternion.identity);

			instantiatedPrefab.transform.parent = transform.parent.transform;
			instantiatedPrefab.transform.SetLocalPositionAndRotation(ninjaStar.transform.position, ninjaStar.transform.rotation);
			//gripPoint = instantiatedPrefab.transform.GetChild(0).position;
			//instantiatedPrefab.transform.position = gripPoint;

			isGrabbed = true;
		}

		if (isGrabbed && triggerValue <= 0.01f && gripValue <= 0.1f)
		{
			ThrowObject();
			isGrabbed = false;
		}
		//log hand position to previous
		previousHandPosition = transform.parent.position;

	}

	private void ThrowObject()
	{
		Debug.Log("thrown");
		Rigidbody rb = instantiatedPrefab.GetComponent<Rigidbody>();
		//Rigidbody handRB = transform.parent.GetComponent<Rigidbody>();
		//Vector3 velocity = handRB.velocity;
		Debug.Log(velocity);
		//rb.isKinematic = false;
		instantiatedPrefab.transform.parent = transform.parent.parent.parent.transform;
		rb.AddForce(velocity * throwForce, ForceMode.Impulse);

	}
}
