using System;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private float actualXChoice, actualZChoice;
    private GameObject player;
    public String playerName;
    public String playerName2;
    
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

private void CreatePlayer()
{
    Debug.Log("Creating player");

    //RandomValueGenerator();
    
    Vector3 location = new Vector3(0,0,0);

    // Check if this player is the first or second player in the room
    if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
    { 
    	location = new Vector3(1.5f,0f,5f);
    }
    
    else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
    {
    	location = new Vector3(12.5f,0f,5f);
    }
    //{
        // Instantiate the first prefab for the first player
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerName), 
            location, Quaternion.identity);
    //}
    /*
    else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
    {
        // Instantiate a different prefab for the second player
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerName2), 
            new Vector3(actualXChoice, 0, actualZChoice), Quaternion.identity);
    }
    else
    {
        // For any additional players, you can assign other prefabs or handle it differently
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerName), 
            new Vector3(actualXChoice, 0, actualZChoice), Quaternion.identity);
    }
    */
    PhotonView photonView = player.GetComponent<PhotonView>();

    // If this is the local player, activate the camera; otherwise, deactivate it
    if (photonView.IsMine)
    {
        Debug.Log("Activating local player camera");
        // Ensure the XR rig/camera for this player is active
        player.GetComponentInChildren<Camera>().enabled = true;
        player.GetComponentInChildren<AudioListener>().enabled = true;
    }
    else
    {
        Debug.Log("Deactivating remote player camera");
        // Disable camera and audio listener for remote players
        player.GetComponentInChildren<Camera>().enabled = false;
        player.GetComponentInChildren<AudioListener>().enabled = false;
    }
}



    private void RandomValueGenerator()
    {
        float xPos = Random.Range(0f, 10f);
        float zNeg = Random.Range(0, 5f);
        
        actualXChoice = xPos;
        actualZChoice = zNeg;
        
    }
}
