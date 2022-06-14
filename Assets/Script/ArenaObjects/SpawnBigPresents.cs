using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnBigPresents : MonoBehaviourPunCallbacks
{

    [Header("----Spawns List----")]
    public Transform[] _spawns;
    private int[] _spawnPosition1 = {1, 4, 5, 8, 0, 2, 10, 1, 7, 4, 3, 9, 2, 11 };
    private int[] _spawnPosition2 = {4, 3, 5, 6, 7, 1, 2, 4, 5, 8, 9, 0, 2, 12 };
    private int[] _spawnPosition3 = {9, 6, 1, 3, 2, 10, 7, 2, 9, 11, 8, 1, 2, 0 };
    private int[] _spawnPosition4 = {5, 1, 0, 8, 3, 4, 2, 4, 7, 1, 4, 7, 2, 3 };
    private int[] _spawnPosition5 = {4, 6, 2, 5, 7, 1, 12, 5, 3, 2, 5, 9, 4, 7 };
    private int[] _spawnPosition6 = {3, 7, 5, 9, 8, 0, 3, 7, 4, 8, 0, 1, 5, 8 };
    private int[] _spawnPosition7 = {2, 5, 9, 1, 6, 12, 0, 2, 5, 1, 7, 2, 6, 9 };
    private int[] _spawnPosition8 = {8, 7, 1, 6, 7, 11, 5, 4, 8, 2, 3, 5, 7, 10 };

    [Header("----Spawns Attribute----")]
    [SerializeField] [Range(10, 60)] private int _spanwTime;
    private bool _isAlready;
    private bool _isChoicing;
    private int _spawnListChoice;
    private int _indexChoiceSpawn;

    [Header("----Present Prefab----")]
    [SerializeField] private string _bigPresentPrefab;

    // Verify if has a big present
    void Update()
    {
        // Get Spawn Postion List
        if (!_isAlready && !_isChoicing)
        {
            _isChoicing = true;
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("ChoiceSpawnList", RpcTarget.AllBuffered, Random.Range(1, 8));
            }
        }

        if (!GameController.Instance._hasBigPresent && _isAlready)
        {
            StartCoroutine("CreateANewPresent");
        }
    }
    
    // Create a Big present
    IEnumerator CreateANewPresent()
    {
        GameController.Instance._hasBigPresent = true;
        yield return new WaitForSeconds(_spanwTime);
        
        _indexChoiceSpawn += 1;
        CreatePresentObject();
    }

    [PunRPC]
    private void ChoiceSpawnList(int index) 
    {
        _isAlready = true;
        _spawnListChoice = index;
    }

    private void CreatePresentObject()
    {
        if (GameController.Instance._hasBigPresent)
        {
            switch (_spawnListChoice)
            {
                case 1:
                    PhotonNetwork.Instantiate(_bigPresentPrefab, _spawns[_spawnPosition1[_indexChoiceSpawn]].position, Quaternion.identity);
                    break;
                case 2:
                    PhotonNetwork.Instantiate(_bigPresentPrefab, _spawns[_spawnPosition2[_indexChoiceSpawn]].position, Quaternion.identity);
                    break;
                case 3:
                    PhotonNetwork.Instantiate(_bigPresentPrefab, _spawns[_spawnPosition3[_indexChoiceSpawn]].position, Quaternion.identity);
                    break;
                case 4:
                    PhotonNetwork.Instantiate(_bigPresentPrefab, _spawns[_spawnPosition4[_indexChoiceSpawn]].position, Quaternion.identity);
                    break;
                case 5:
                    PhotonNetwork.Instantiate(_bigPresentPrefab, _spawns[_spawnPosition5[_indexChoiceSpawn]].position, Quaternion.identity);
                    break;
                case 6:
                    PhotonNetwork.Instantiate(_bigPresentPrefab, _spawns[_spawnPosition6[_indexChoiceSpawn]].position, Quaternion.identity);
                    break;
                case 7:
                    PhotonNetwork.Instantiate(_bigPresentPrefab, _spawns[_spawnPosition7[_indexChoiceSpawn]].position, Quaternion.identity);
                    break;
                case 8:
                    PhotonNetwork.Instantiate(_bigPresentPrefab, _spawns[_spawnPosition8[_indexChoiceSpawn]].position ,Quaternion.identity);
                    break;
            }
        }   
    }
}
