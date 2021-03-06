using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    public List<PlayerMoviment> _jogadores;
    public List<PlayerMoviment> Jogadores { get => _jogadores; private set => _jogadores = value; }

    public int _playerLogMineId;
    
    // Big Present
    public bool _hasBigPresent;

    // GameOver Menu
    public GameOverMenu _gameOverMenu;


    //Add a Player
    private void Start()
    {
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
        _jogadores = new List<PlayerMoviment>();
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
        var player = playerObj.GetComponent<PlayerMoviment>();
        // Initialize Player And Pass A Local Player as Parameter
        player.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

}
