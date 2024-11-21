using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FixedDistanceGrab : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    void Start()
    {
        if (grabInteractable == null)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        grabInteractable.throwOnDetach = false; // Disable throw to prevent unintended movement
        // Assuming manipulation moveProvider might be attached to an XR base controller
        DisableMoveProvider(args.interactor);
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        EnableMoveProvider(args.interactor);
    }

    void DisableMoveProvider(IXRSelectInteractor interactor)
    {
        if (interactor is XRBaseControllerInteractor controllerInteractor)
        {
            controllerInteractor.xrController.enableInputTracking = false; // This should stop joystick input affecting the object
        }
    }

    void EnableMoveProvider(IXRSelectInteractor interactor)
    {
        if (interactor is XRBaseControllerInteractor controllerInteractor)
        {
            controllerInteractor.xrController.enableInputTracking = true;
        }
    }
}
