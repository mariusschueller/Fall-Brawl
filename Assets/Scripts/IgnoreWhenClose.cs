using UnityEngine;

public class IgnoreWhenClose : MonoBehaviour
{
    public Camera mainCamera; // Assign the camera in the Inspector
    public Renderer modelRenderer; // Assign the model's Renderer
    public float ignoreDistance = 1.0f; // Distance to start ignoring the model

    void Update()
    {
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Enable or disable rendering based on distance
        if (distance < ignoreDistance)
        {
            modelRenderer.enabled = false; // Hide model
        }
        else
        {
            modelRenderer.enabled = true; // Show model
        }
    }
}

