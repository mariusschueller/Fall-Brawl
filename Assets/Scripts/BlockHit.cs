using Photon.Pun;
using UnityEngine;

public class BlockHit : MonoBehaviourPunCallbacks
{
    public bool isThrown;
    public float knockbackForce = 10f;
    private BoxCollider bc;
    private Rigidbody rb;
    public GameObject particleEffectPrefab;

    private void Start()
    {
        isThrown = false;
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
{
    // Check if the object is above the threshold height
    if (transform.position.y > -1f)
    {
        PhotonView photonView = GetComponent<PhotonView>();

        // Only the MasterClient can destroy the object
        if (PhotonNetwork.IsMasterClient)
        {
            if (photonView != null)
            {
                // Check if the object is instantiated via Photon
                if (photonView.Owner == null || photonView.IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
        else
        {
            Debug.LogWarning("Only the MasterClient can destroy objects in this setup.");
        }

        // Instantiate particle effect locally
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}


    public void DisableCollider()
    {
    	bc.enabled = false;
        photonView.RPC("SyncDisableCollider", RpcTarget.All);
    }

    public void EnableCollider()
    {
    	bc.enabled = true;
        photonView.RPC("SyncEnableCollider", RpcTarget.All);
    }

    [PunRPC]
    private void SyncDisableCollider()
    {
        bc.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    [PunRPC]
    private void SyncEnableCollider()
    {
        bc.enabled = true;
        rb.constraints = RigidbodyConstraints.None;
    }
}

