using Photon.Pun;
using UnityEngine;

public class GameOver : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        	Debug.Log("Game Over Triggered!");
            PhotonView photonView = other.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                // Only the player who collides with the trigger sends the RPC
                photonView.RPC("GameOver", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void GameOverRun()
    {
        // Code to execute game-over logic for each player
        Debug.Log("Game Over!");
        // Show win or lose UI here based on current y
        
    }
}

