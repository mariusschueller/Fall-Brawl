using UnityEngine;
using Photon.Pun;

public class NetworkingController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        // just connecting to the photon network, change this if you do your own server
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master " + PhotonNetwork.CloudRegion + " server!");
    }

}