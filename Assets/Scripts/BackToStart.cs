using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke("toStart",5f);
    }

    
    void toStart()
    {
        SceneManager.LoadScene(0);
    }
}
