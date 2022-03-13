using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GestorDeRede : MonoBehaviourPunCallbacks
{
    // Create A Instance of GestorDeRede
    public static GestorDeRede Instance;
    // Check If Instance Has Already been created
    private void Awake()
    {
        if(Instance != null && Instance != this) 
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Called When User Connect With The Server
    private void OnConnectedToServer()
    {
        
    }
}
