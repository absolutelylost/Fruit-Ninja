using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHook : MonoBehaviour
{
	public InputActionProperty pinchAnimationAction;
	public InputActionProperty gripAnimationAction;
    public InputActionProperty xyButtonPress;

    [SerializeField] private GameObject xrOrigin;
	[SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject grappleHand;
    public LayerMask grappleable;
    public GameObject grappleTargetImage;
    private GameObject marker;
	public float grapplingDelayTime;
    private Vector3 grapplePoint;
    private bool isGrappling = false;
    private bool grapplingSelect = false;
	private float grapplingTime = 0;
	private float grapplingTimeLimit = 3;
	private float grapplingSpeed = 3.0f;


	// Start is called before the first frame update
	void Start()
    {
		//lineRenderer = transform.GetComponent<LineRenderer>();
        //Ray ray = new Ray(transform.position, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
		float triggerValue = pinchAnimationAction.action.ReadValue<float>();

		float gripValue = gripAnimationAction.action.ReadValue<float>();
        
        bool xyIsPressed = xyButtonPress.action.IsPressed();
        // if time runs out reset timer and stop grapple selection
        CheckStopGrappleSelect();

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, 20f, grappleable))
        {
            if(marker == null)
            {
				grapplePoint = hitInfo.point;
				marker = Instantiate(grappleTargetImage, grapplePoint, hitInfo.collider.transform.rotation);

            }
            else
            {
                //place marker for targeting grappling
				marker.transform.position = hitInfo.point;
                marker.transform.rotation = Quaternion.LookRotation(hitInfo.normal);

                //control position and visibility of grappling shot
				if(triggerValue > .9f && !isGrappling && !grapplingSelect)
                {
                    ActivateGrappleLine(hitInfo);
				}
                else if (triggerValue < .1f && isGrappling && grapplingSelect)
                {
                    grapplingSelect = false;

					lineRenderer.enabled = false;

				}
				//keep grappling connected to hand
				if (grapplingSelect)
                {
                    StartGrappleSelect();
                    if (xyIsPressed)
                    {
                        //restart grapple time
                        grapplingTime = 0;
                        isGrappling = true;

					}
				}
			}
        }
        else
        {
           Destroy(marker);
        }

        if (isGrappling)
        {
            xrOrigin.transform.position = Vector3.Lerp(xrOrigin.transform.position, grapplePoint, grapplingSpeed * Time.deltaTime);
        }
    }

    public void ActivateGrappleLine(RaycastHit hitInfo)
    {
		lineRenderer.enabled = true;
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, hitInfo.point);
		grapplingSelect = true;

	}

    // if gripped for too much time, reset
	private void CheckStopGrappleSelect()
    {
		if (grapplingTime >= grapplingTimeLimit)
		{
            ResetGrappling();
		}
	}

    public void ResetGrappling()
    {
		grapplingSelect = false;
		grapplingTime = 0;
		isGrappling = false;
		lineRenderer.enabled = false;


	}

	private void StartGrappleSelect()
    {
		lineRenderer.SetPosition(0, transform.position);
		grapplingTime += Time.deltaTime;

	}

}
