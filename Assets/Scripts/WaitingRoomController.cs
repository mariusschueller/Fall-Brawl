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
    private int selectNum;
    [SerializeField] private GameObject spaceSelect;
    [SerializeField] private GameObject waterSelect;
    
    [SerializeField] private Button startButton;
    
    private bool sceneSet;
    
    private bool startingGame;
    private int minPlayers = 1;
    
    private Vector3 rotation;
    
    void Update() 
    {
    	if (selectNum == 1)
    	{
    		//rotate space
    		 
    		 spaceSelect.transform.Rotate(rotation, Space.Self);
    	} 
    	
    	else if (selectNum == 2)
    	{
    		//rotate water
    		waterSelect.transform.Rotate(rotation, Space.Self);
    	} 
    
    }
    
    // Start is called before the first frame update
    void Start()
    {
    	rotation = new Vector3(0, 50f, 0) * Time.deltaTime;
        myPhotonView = GetComponent<PhotonView>();

        
        
        selectNum = 1;
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
        startButton.interactable = (playerCount >= minPlayers) && selectNum > 0;
        PlayerPrefs.SetInt("select", selectNum);
        
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
    	
    	selectNum = 1;
    	sceneSet = true;
    	PlayerPrefs.SetInt("select", selectNum);
    }
    
    public void SetSceneWater()
    {
    	Debug.Log("Water being set");
    	sceneSet = true;
    	selectNum = 2;
    	PlayerPrefs.SetInt("select", selectNum);
    }
}
