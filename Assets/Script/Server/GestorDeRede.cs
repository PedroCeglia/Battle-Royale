using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    
    // Join A Random Room
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    // Listener ( When dont have exist room open ) 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }
    // Create Room
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room :" + Random.Range(0, 100000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);  
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
        if (PhotonNetwork.IsMasterClient)
        {
            // Block The Room Acess
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    }

    public void BackToIntroScene()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("IntroScene");
        Destroy(gameObject);
    }
}
