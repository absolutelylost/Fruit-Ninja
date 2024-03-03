using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;
	[SerializeField] private GameObject sword;
	[SerializeField] private GameObject ninjaStar;
	[SerializeField] private TextMeshProUGUI StarValue;
	[SerializeField] private GameObject headCamera;

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
		//update number of stas available
		currentHandPosition = transform.parent.position;
		velocity = (currentHandPosition - previousHandPosition) / Time.deltaTime;

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
			if(numThrowingStars > 0)
			{
				instantiatedPrefab = Instantiate(ninjaStar, transform.position, Quaternion.identity);
				instantiatedPrefab.transform.parent = transform.parent.transform;
				instantiatedPrefab.transform.SetLocalPositionAndRotation(ninjaStar.transform.position, ninjaStar.transform.rotation);
				
				isGrabbed = true;
				numThrowingStars--;
				Debug.Log(StarValue.text);
				StarValue.text = numThrowingStars.ToString();
				Debug.Log(numThrowingStars.ToString());

			}
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
		Rigidbody rb = instantiatedPrefab.GetComponent<Rigidbody>();
		instantiatedPrefab.transform.parent = transform.parent.parent.parent.transform;
		instantiatedPrefab.transform.rotation = headCamera.transform.rotation;

		if(velocity.magnitude > 2)
		{
			rb.AddForce(headCamera.transform.forward * velocity.magnitude, ForceMode.Impulse);
			instantiatedPrefab.GetComponent<StarBehavior>().StartTimer();
		}
		else
		{
			rb.useGravity = true;
		}

	}
}
