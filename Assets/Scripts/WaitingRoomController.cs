using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class WaitingRoomController : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    
    [SerializeField] private int multiplayerSceneIndex;
    [SerializeField] private int menuSceneIndex;

    private int playerCount;
    private int roomSize;

    [SerializeField] private TextMeshProUGUI roomCountDisplay;
    [SerializeField] private TextMeshProUGUI waitingForHost;
    
    [SerializeField] private GameObject selects;
    
    [SerializeField] private Button startButton;
    
    private bool sceneSet;
    
    private bool startingGame;
    public int minPlayers = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();

        PlayerCountUpdate();
        
        sceneSet = false;
        
        // if client then show start button
        if (PhotonNetwork.IsMasterClient)
        {
            selects.gameObject.SetActive(true);
            startButton.gameObject.SetActive(true);
            waitingForHost.gameObject.SetActive(false);
            
        }
        
        
    }

    void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = "Players Joined: " + playerCount;
        
        // check that at least 2 people are in the room and then set start to interactable if client
        startButton.interactable = (playerCount >= minPlayers) && sceneSet;
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    public void StartGame()
    {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
    
    public void SetSceneSpace()
    {
    	Debug.Log("Space being set");
    	sceneSet = true;
    }
    
    public void SetSceneWater()
    {
    	Debug.Log("Water being set");
    	sceneSet = true;
    }
}
