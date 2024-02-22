using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
	private bool holdingThrowable;

	// Start is called before the first frame update
	void Start()
    {
		holdingThrowable = false;

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
		if (triggerValue > 0.9f && gripValue < 0.1f && !holdingThrowable)
		{
			GameObject instantiatedPrefab = Instantiate(ninjaStar, transform.position, transform.rotation);
			holdingThrowable = true;
		}
		else
		{
			holdingThrowable = false;
		}
	}
}
