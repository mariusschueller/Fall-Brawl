using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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

	    
            Vector3 originalPos = transform.position;
            originalPos.y = originalPos.y - 2;
            PhotonNetwork.Instantiate(speedCircle.name, transform.position, Quaternion.identity);

            // Destroy the power-up object across all clients
            PhotonNetwork.Destroy(gameObject);
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
