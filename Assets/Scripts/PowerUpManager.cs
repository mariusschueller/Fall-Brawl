using System.Collections;
using System.Collections.Generic;

using UnityEngine.XR;

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PowerUpManager : MonoBehaviour
{
    public GameObject speedCircle;
    public float speedMultiplier = 1.5f; 
    public float powerUpDuration = 5f;
    private bool isSpeedActive = false;
    
    
    private PlayerMovement pm;
    
    //private ContinuousMoveProviderBase moveProvider; // reference to move provider
    private float originalMoveSpeed;

    
    private void OnTriggerEnter(Collider other)
    {
    	
    	
        if (other.CompareTag("Player") && !isSpeedActive)
        {
            Debug.Log("Entering!");
            pm = other.gameObject.GetComponent<PlayerMovement>();
            
            ActivateSpeed();

	    // Photon instantiate?
            Instantiate(speedCircle, transform.position, Quaternion.identity); 

            // Do photon destroy?
            Destroy(this.gameObject);
        }
    }

    private void ActivateSpeed()
    {
        pm.PowerupStart();
        Invoke("DeactivateSpeed", powerUpDuration); 
    }

    private void DeactivateSpeed()
    {
        pm.PowerupEnd();
    }
}
