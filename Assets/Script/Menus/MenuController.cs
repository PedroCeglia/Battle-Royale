using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MenuController : MonoBehaviourPunCallbacks
{
    // Menus
    [SerializeField] IntroMenu _menuIntro;
    [SerializeField] LobbyMenu _menuLobby;
    [SerializeField] LoadRoomMenu _menuLoadRoom;
    [SerializeField] LogMenu _menuLog;

    // Atributes
    private bool _isIntroRoom;
    private bool _isLogRoom;
    private bool _isLobbyRoom;
    private bool _isLoadRoom;
    private bool _isServerConnect;

    // Set The Menu to MenuLoadRoom
    void Start()
    {
        SetMenuActive("intro");
    }

    // Manager Menus
    private void Update()
    {
        // If Intro Menu Is Active
        if (_isIntroRoom)
        {
            // If Firebase Is Connect And Photon Is Connect
            if(_isServerConnect && FirebaseConfig.Instance._isConnect)
            {
                _isIntroRoom = false;
            }
        }
        else
        {
            // If User Is Log
            if(Auth.Instance._user != null)
            {
                // If Lobby Menu Is Active
                if (!_isLobbyRoom && !_isLoadRoom)
                {
                    SetMenuActive("lobby");
                }
            }
            else
            {
                // If Log Menu Is Active
                if (!_isLogRoom)
                {
                    SetMenuActive("log");
                }
            }
        }

    }

    // Called When The User Connect With The Server
    public override void OnConnectedToMaster()
    {
        _isServerConnect = true;
    }

    // Set menu Active
    private void SetMenuActive(string menu)
    {
        // Disactive Menus
        _menuIntro.gameObject.SetActive(false);
        _menuLobby.gameObject.SetActive(false);
        _menuLoadRoom.gameObject.SetActive(false);
        _menuLog.gameObject.SetActive(false);

        // Reset Bool Menu Attributes
        _isIntroRoom = false;
        _isLobbyRoom = false;
        _isLoadRoom = false;
        _isLogRoom = false;

        // Active Menu Choiced
        switch (menu)
        {
            case "intro":
                _menuIntro.gameObject.SetActive(true);
                _isIntroRoom = true;
                break;
            case "lobby":
                _menuLobby.gameObject.SetActive(true);
                _isLobbyRoom = true;
                break;
            case "load":
                _menuLoadRoom.gameObject.SetActive(true);
                _isLoadRoom = true;
                break;
            case "log":
                _menuLog.gameObject.SetActive(true);
                _isLogRoom = true;
                break;
        }
    }

    // Called When The User Join In a Room
    public override void OnJoinedRoom()
    {
        SetMenuActive("load");
        Debug.Log(PhotonNetwork.PlayerList.Length);
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            // Create Room In Firebase

            // Create Room In Photon
            GestorDeRede.Instance.photonView.RPC("StartTheGame", RpcTarget.All);
        }
        else if (PhotonNetwork.PlayerList.Length > 2)
        {
            UserLeaveRoom();
        }
    }

    // User Leave The Room
    public void UserLeaveRoom()
    {
        GestorDeRede.Instance.UserLeaveRoom();
        SetMenuActive("lobby");
    }

    // Join A Random Room
    public void JoinRoom()
    {
        GestorDeRede.Instance.JoinRandomRoom();
    }
}
