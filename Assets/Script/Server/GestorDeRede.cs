using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GestorDeRede : MonoBehaviourPunCallbacks
{
    // Create A Instance of GestorDeRede
    public static GestorDeRede Instance { get; private set; }
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
    // Connect With The Server
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    // Create Or Join A Random Room
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    // User Leave The Room
    public void UserLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // Start The Game To All Users
    [PunRPC]
    public void StartTheGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
