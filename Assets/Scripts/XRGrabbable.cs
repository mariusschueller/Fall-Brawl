using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

[RequireComponent(typeof(XRGrabInteractable))]
public class XRGrabbable : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public  UnityEvent grabEvent;
    public  UnityEvent releaseEvent;
    
    private void Awake()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Register grab and release event listeners
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        // Unregister event listeners
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Logic to execute when the object is grabbed
        Debug.Log($"{gameObject.name} grabbed by {args.interactorObject.transform.name}");
        grabEvent.Invoke();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
    	releaseEvent.Invoke();
        // Logic to execute when the object is released
        Debug.Log($"{gameObject.name} released by {args.interactorObject.transform.name}");
    }
}

