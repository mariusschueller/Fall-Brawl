using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRayInteractorCustomDistance : MonoBehaviour
{
    [SerializeField]
    private XRRayInteractor rayInteractor;

    [SerializeField]
    private float customDistance = 4.0f; // Distance to keep the grabbed object

    private XRBaseInteractable grabbedObject;
    private bool isGrabbing = false;

    private void OnEnable()
    {
        if (rayInteractor == null)
            rayInteractor = GetComponent<XRRayInteractor>();

        rayInteractor.selectEntered.AddListener(OnGrab);
        rayInteractor.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        rayInteractor.selectEntered.RemoveListener(OnGrab);
        rayInteractor.selectExited.RemoveListener(OnRelease);
    }

    private void Update()
    {
        if (isGrabbing && grabbedObject != null)
        {
            // Manually set the position of the grabbed object
            Vector3 rayOrigin = rayInteractor.transform.position;
            Vector3 rayDirection = rayInteractor.transform.forward;

            // Calculate the target position
            Vector3 targetPosition = rayOrigin + rayDirection * customDistance;

            // Move the grabbed object to the target position
            grabbedObject.transform.position = targetPosition;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (args.interactableObject != null)
        {
            grabbedObject = args.interactableObject as XRBaseInteractable;
            isGrabbing = true;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        grabbedObject = null;
        isGrabbing = false;
    }
}

