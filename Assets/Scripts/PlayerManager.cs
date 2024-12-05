using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Unity.XR.CoreUtils;

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;

    [Header("Player Components")]
    public Camera playerCamera;
    public XROrigin xrOrigin;
    public InputActionManager inputActionManager;
    public int activeNum;
    public PlayerMovement pm;
    public GameObject self;
    public SkinnedMeshRenderer smr;
    private float knockbackForce = 20f;
    public bool hideplayer = true;
    int actorNumber;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
    actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        if (actorNumber == activeNum)
        {
            EnableLocalPlayerComponents();
        }
        else
        {
            DisableRemotePlayerComponents();
        }
    }

    private void EnableLocalPlayerComponents()
    {
        Debug.Log("Enabling local player components.");

        if (playerCamera != null)
            playerCamera.enabled = true;

        if (xrOrigin != null)
            xrOrigin.enabled = true;

        if (inputActionManager != null)
            inputActionManager.enabled = true;
            
        if (pm != null)
            pm.enabled = true;
            
        if (hideplayer)
            smr.enabled = false;

        // Enable any other player-specific components here.
    }

    private void DisableRemotePlayerComponents()
    {
        Debug.Log("Disabling remote player components.");

        if (playerCamera != null)
            playerCamera.enabled = false;

        if (xrOrigin != null)
            xrOrigin.enabled = false;

        if (pm != null)
            pm.enabled = false;
            
        self.SetActive(false);
        
        smr.enabled = true;
           

        // Disable any other player-specific components here.
    }
    
    private void OnCollisionEnter(Collision collision){
    if (actorNumber == activeNum)
        {
    if (collision.gameObject.CompareTag("ground") && collision.gameObject.transform.position.y > -1f)
    {
        
            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }
    }
}

