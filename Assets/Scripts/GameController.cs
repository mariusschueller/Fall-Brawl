using System;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
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
    
    void Start()
    {
        CreatePlayer();
        SpawnPowerUp(); // Start spawning power-ups
        if (PlayerPrefs.GetInt("select") == 2) {
        	RenderSettings.skybox = greenSkybox;
        }
        else {
        	RenderSettings.skybox = blueSkybox;
        }
    }

    private void CreatePlayer()
    {
        
        Debug.Log("Creating player");
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if (actorNumber == 1)
        {
            Vector3 location = new Vector3(1.5f, 0f, 5f);
            GameObject player1 = PhotonNetwork.Instantiate(playerPrefab.name, location, Quaternion.identity, 0);
            player1.GetComponentInChildren<Camera>(true).enabled = true;
        }
        
        if (actorNumber == 2)
        {
            Vector3 location = new Vector3(12.5f, 0f, 5f);
            GameObject player2 = PhotonNetwork.Instantiate(playerPrefab.name, location, Quaternion.identity, 0);
            player2.GetComponentInChildren<Camera>(true).enabled = true;
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
        Invoke(nameof(SpawnPowerUp), 20f);
    }
}

