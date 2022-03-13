using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuController : MonoBehaviourPunCallbacks
{
    // Menus
    [SerializeField] GameObject _menuIntro;
    [SerializeField] GameObject _menuLobby;
    [SerializeField] GameObject _menuLoadRoom;

    // Start is called before the first frame update
    void Start()
    {
        _menuIntro.SetActive(true);
        _menuLobby.SetActive(false);
        _menuLoadRoom.SetActive(false);
    }

    // Called When The User Connect With The Server
    public override void OnConnectedToMaster()
    {
        // Open Menu Lobby
        SetMenuActive(_menuLobby);
    }

    // Set menu Active
    private void SetMenuActive(GameObject _menu)
    {
        // Disactive Menus
        _menuIntro.SetActive(false);
        _menuLobby.SetActive(false);
        _menuLoadRoom.SetActive(false);

        // Active Menu Choiced
        _menu.SetActive(true);
    }



}
