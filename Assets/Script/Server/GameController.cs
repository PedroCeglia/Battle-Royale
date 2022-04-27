using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
{
    // Create A Instance 
    public static GameController Instance { get; private set; }
    
    // Create A Singleton
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

    // Initialize Player Atributes
    [SerializeField] private string _localizacaoPrefab;
    [SerializeField] private Transform[] _spawnList;

    // Players List
    private int _playersNumber = 0;
    public List<Player> _jogadores;
    public List<Player> Jogadores { get => _jogadores; private set => _jogadores = value; }


    //Add a Player
    private void Start()
    {
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
        _jogadores = new List<Player>();
    }

    // Add Player
    [PunRPC]
    private void AddPlayer()
    {
        _playersNumber++;
        if (_playersNumber == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }

    // Create Player
    private void CreatePlayer()
    {
        // Create a Player Instance using Photon
        var playerObj = PhotonNetwork.Instantiate(_localizacaoPrefab, _spawnList[Random.Range(0, _spawnList.Length)].position, Quaternion.identity);
        var player = playerObj.GetComponent<Player>();
        // Initialize Player And Pass A Local Player as Parameter
        player.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
