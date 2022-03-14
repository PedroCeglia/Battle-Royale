using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
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

}
