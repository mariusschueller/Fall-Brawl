using Photon.Pun;
using UnityEngine;

public class BlockHit : MonoBehaviourPunCallbacks
{
	public bool isThrown;
	public float knockbackForce = 10f;
	private BoxCollider bc;
	private Rigidbody rb;
	public GameObject particleEffectPrefab;
	
	private void Start(){
	
		isThrown = false;
		bc = GetComponent<BoxCollider>();
		rb = GetComponent<Rigidbody>();
		
	}
    private void OnCollisionEnter(Collision collision)
    {
    	
    	
    	
        // Check if the collision is with a player
        if (collision.gameObject.CompareTag("Player") && transform.position.y > -1f)
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
            // Only the owner or master client should call Destroy to prevent duplicate calls
            
        }
        if (transform.position.y > -1f)
        {
        	if (PhotonNetwork.IsMasterClient)
	    {
		PhotonNetwork.Destroy(gameObject);
	    }
	    if (particleEffectPrefab != null)
	    {
		Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
	    }
	}
    }
    
    public void disableCollider()
    {
    	bc.enabled = false;
    }
    
    public void enableCollider()
    {
    	bc.enabled = true;
    	rb.constraints = RigidbodyConstraints.None;
    }
}

