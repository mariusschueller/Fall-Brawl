using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;

public class PhotonPlayerManager : MonoBehaviourPun
{
    public GameObject Camera;
    
    private PhotonView pv;
    
    
    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            
        }
    }

    

    
    
}