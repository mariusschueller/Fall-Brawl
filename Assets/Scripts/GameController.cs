using System;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private float actualXChoice, actualZChoice;
    private GameObject player;
    public string playerName;
    public string playerName2;
    public GameObject powerUpPrefab; // Assign this in the Unity Editor or instantiate a prefab path
    public Vector2 spawnAreaMin = new Vector2(0f, 0f); // Minimum bounds of the spawn area
    public Vector2 spawnAreaMax = new Vector2(15f, 10f); // Maximum bounds of the spawn area
    public Material greenSkybox;
    public Material blueSkybox;

    [SerializeField]
    private GameObject playerPrefab;
    
    [SerializeField]
    private GameObject player2Prefab;
    
    void Start()
    {
        SpawnPlayer();
        //SpawnPowerUp(); // Start spawning power-ups
        if (PlayerPrefs.GetInt("select") == 2) {
        	RenderSettings.skybox = greenSkybox;
        }
        else {
        	RenderSettings.skybox = blueSkybox;
        }
    }
    
    private void SpawnPlayer()
    {
        Debug.Log("Creating player");
    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
    GameObject playerInstance;

    if (actorNumber == 1)
    {
        Vector3 location = new Vector3(1.5f, 0f, 5f);
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, location, Quaternion.identity, 0);
    }
    else
    {
        Vector3 location = new Vector3(12.5f, 0f, 5f);
        playerInstance = PhotonNetwork.Instantiate(player2Prefab.name, location, Quaternion.identity, 0);
    }
    }

    

// Utility method to enable/disable ActionBasedController components
private void EnableControllerComponents(GameObject playerInstance, bool enable)
{
    // Find the Camera Offset GameObject
    var cameraOffset = playerInstance.transform.Find("Camera Offset");

    if (cameraOffset != null)
    {
        cameraOffset.gameObject.SetActive(enable); // Enable or disable the Camera Offset
    }
    else
    {
        Debug.LogWarning("Camera Offset not found on the player instance.");
    }

    // Optionally handle other components like XR Origin or Input Action Manager
    var xrOrigin = playerInstance.GetComponentInChildren<XROrigin>();
    if (xrOrigin != null)
    {
        xrOrigin.enabled = enable;
    }

    var inputActionManager = playerInstance.GetComponentInChildren<InputActionManager>();
    if (inputActionManager != null)
    {
        inputActionManager.enabled = enable;
    }
    
    var playerController = playerInstance.GetComponentInChildren<PlayerMovement>();
    if (playerController != null)
    {
        playerController.enabled = enable;
    }
}





    private void SpawnPowerUp()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            0.1f, // Assuming the power-up spawns slightly above the ground
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        if (PhotonNetwork.IsMasterClient) // Ensure only the master client spawns the power-ups
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", powerUpPrefab.name),
                spawnPosition, Quaternion.identity);
        }

        // Schedule the next spawn
        Invoke(nameof(SpawnPowerUp), 30f);
    }
}

