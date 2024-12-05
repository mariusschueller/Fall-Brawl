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

