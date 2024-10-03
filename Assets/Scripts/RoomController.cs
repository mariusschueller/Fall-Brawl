using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks
{
    
    [SerializeField] 
    private int waitingRoomerSceneIndex;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(waitingRoomerSceneIndex);
    }
}
