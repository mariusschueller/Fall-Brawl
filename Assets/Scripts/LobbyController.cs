using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using System.Text.RegularExpressions;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] 
    private GameObject StartButton;
    
    [SerializeField]
    private GameObject CancelButton;
    
    [SerializeField]
    private Button JoinCodeButton;
    
    [SerializeField]
    private TextMeshProUGUI joinCodeText;

    [SerializeField] 
    private int roomSize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        StartButton.SetActive(true);
    }

    public void DelayStart()
    {
        StartButton.SetActive(false);
        CancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Joined Random Room");
    }
    
    public void DelayCancel()
    {
        CancelButton.SetActive(false);
        StartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("Creating Room");
        int randomRoomNumber = Random.Range(1, 10000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOptions);
        Debug.Log("Created Room" + randomRoomNumber);
    }
    
    

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room, trying again");
        CreateRoom();
    }

    void Update()
    {
        JoinCodeButton.interactable = Regex.IsMatch(joinCodeText.text, @"[a-zA-Z0-9]");
    }
}
