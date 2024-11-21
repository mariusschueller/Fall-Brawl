using System.Collections;
using System.Collections.Generic;

using UnityEngine.XR;

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PowerUpManager : MonoBehaviour
{
    public GameObject speedCircle; // assign the speed circle prefab in the inspector
    public float speedMultiplier = 1.5f; // factor to increase movement speed for speed
    public float powerUpDuration = 15f; // duration of power-up effects in seconds
    private bool isSpeedActive = false;

    private ContinuousMoveProviderBase moveProvider; // reference to move provider
    private float originalMoveSpeed;

    void Start()
    {
        // get reference to movement provider to adjust speed
        moveProvider = FindObjectOfType<ContinuousMoveProviderBase>();
        if (moveProvider != null)
        {
            originalMoveSpeed = moveProvider.moveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the player collides with the "Lightning" tagged object
        // if (other.CompareTag("PowerUp") && other.gameObject.name.Contains("Lightning") && !isSpeedActive)
        if (other.CompareTag("Player") && other.gameObject.name.Contains("XR Origin (XR Rig)") && !isSpeedActive)
        {
            Debug.Log("Entering!");
            ActivateSpeed();
            Instantiate(speedCircle, transform.position, Quaternion.identity); // spawn the speed circle around the player

            // destroy the sphere after activation
            // Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void ActivateSpeed()
    {
        if (isSpeedActive || moveProvider == null) return; // prevent multiple activations
        isSpeedActive = true;
        moveProvider.moveSpeed *= speedMultiplier; // increase movement speed
        Invoke("DeactivateSpeed", powerUpDuration); // deactivate after duration
    }

    private void DeactivateSpeed()
    {
        isSpeedActive = false;
        if (moveProvider != null)
        {
            moveProvider.moveSpeed = originalMoveSpeed; // reset movement speed
        }
    }
}
