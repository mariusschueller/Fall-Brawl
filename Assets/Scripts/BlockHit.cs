using Photon.Pun;
using UnityEngine;

public class BlockHit : MonoBehaviourPunCallbacks
{
	public bool isThrown;
	public float knockbackForce = 10f;
	
	private void Start(){
	
		isThrown = false;
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
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}

