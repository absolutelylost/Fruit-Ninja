using System.Collections;
using System.Collections.Generic;
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
	private Vector3 gripPoint;
	private bool isGrabbed;
	private GameObject instantiatedPrefab;
	private float throwForce = 5.0f;
	private Vector3 throwingVelocity;

	// Start is called before the first frame update
	void Start()
    {
		isGrabbed = false;
	}

    // Update is called once per frame
    void Update()
    {
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
			if (ninjaStar == null) return;
			//instantiatedPrefab = Instantiate(ninjaStar, transform.position, transform.rotation);
			//gripPoint = instantiatedPrefab.transform.GetChild(0).position;
			//instantiatedPrefab.transform.position = transform.position;
			ninjaStar.SetActive(true);
			isGrabbed = true;
		}
		else
		{
			if (ninjaStar == null) return;
			ninjaStar.SetActive(false);
		}

		if (isGrabbed && triggerValue <= 0.01f && gripValue >= 0.9f)
		{
			//ThrowObject();
			isGrabbed = false;
		}

	}

	private void ThrowObject()
	{
		Rigidbody rb = ninjaStar.GetComponent<Rigidbody>();
		throwingVelocity = rb.velocity;
		//rb.isKinematic = false;
		rb.AddForce(throwingVelocity * throwForce, ForceMode.Impulse);
	}
}
