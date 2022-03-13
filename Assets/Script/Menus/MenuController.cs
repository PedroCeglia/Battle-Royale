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

    // Start is called before the first frame update
    void Start()
    {
        _menuIntro.gameObject.SetActive(true);
        _menuLobby.gameObject.SetActive(false);
        _menuLoadRoom.gameObject.SetActive(false);
    }

    // Called When The User Connect With The Server
    public override void OnConnectedToMaster()
    {
        // Open Menu Lobby
        SetMenuActive(_menuLobby.gameObject);
    }

    // Set menu Active
    private void SetMenuActive(GameObject _menu)
    {
        // Disactive Menus
        _menuIntro.gameObject.SetActive(false);
        _menuLobby.gameObject.SetActive(false);
        _menuLoadRoom.gameObject.SetActive(false);

        // Active Menu Choiced
        _menu.SetActive(true);
    }
}
