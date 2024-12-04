using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonPlayerManager : MonoBehaviourPun
{
    public GameObject Camera;
    
    private PhotonView pv;
    
    
    // Start is called before the first frame update
    /*void Start()
    {
           pv = GetComponent<PhotonView>();
           if (pv.IsMine)
           {
               Camera.SetActive(true);
           }
           else
           {
               Camera.SetActive(false);
           }
    }*/

    // Update is called once per frame
    //void Update()
    //{
    //    if (pv.IsMine)
    //    {
            
    //    }
    //}
    
    [PunRPC]
    public void GameOver()
    {
        if (photonView.IsMine)
        {
        	Debug.Log("Game Over!");
            // Show the game-over UI on the local player's screen
            //gameOverUI.SetActive(true);
            if (transform.position.y > -1f){
            	SceneManager.LoadScene("YouWin");
            }
            else {
            	SceneManager.LoadScene("GameOver");
            }
        }
    }

    

    
    
}
