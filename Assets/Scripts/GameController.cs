using System;
using UnityEngine;
using Photon.Pun;
using System.IO;
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

    void Start()
    {
        CreatePlayer();
        SpawnPowerUp(); // Start spawning power-ups
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating player");

        Vector3 location = new Vector3(0, 0, 0);

        // Check if this player is the first or second player in the room
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            location = new Vector3(1.5f, 0f, 5f);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            location = new Vector3(12.5f, 0f, 5f);
        }

        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerName),
            location, Quaternion.identity);

        PhotonView photonView = player.GetComponent<PhotonView>();

        // If this is the local player, activate the camera; otherwise, deactivate it
        if (photonView.IsMine)
        {
            Debug.Log("Activating local player camera");
            player.GetComponentInChildren<Camera>().enabled = true;
            player.GetComponentInChildren<AudioListener>().enabled = true;
        }
        else
        {
            Debug.Log("Deactivating remote player camera");
            player.GetComponentInChildren<Camera>().enabled = false;
            player.GetComponentInChildren<AudioListener>().enabled = false;
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

