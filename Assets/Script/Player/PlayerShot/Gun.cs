using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviourPunCallbacks
{
    [Header("Widgets")]
    public GameObject ammunition;
    public Transform areaAttack;
    public GameObject player;
    private Animator _playerAnim;
    private PlayerMoviment _playerMoviment;
    private PlayerHealth _playerHealth;

    [Header("Atrributes")]
    [SerializeField]
    [Range(1, 10)]
    private int _damage;
    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float _fireRate;
    [SerializeField]
    private float _fireRange;
    [SerializeField]
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = player.GetComponent<Animator>();
        _playerMoviment = player.GetComponent<PlayerMoviment>();
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer >= _fireRate)
        {
            if (_playerMoviment.isMine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    photonView.RPC("Attack", RpcTarget.All);
                    _timer = 0f;
                
                }
            }
        }
        else
        {
            player.GetComponent<PlayerMoviment>().isAttack = false;
            _timer += Time.deltaTime;
        }
    }
    [PunRPC]
    public void Attack()
    {
        //
        _playerAnim.SetTrigger("isAttack");
        player.GetComponent<PlayerMoviment>().isAttack = true;
        Transform ammunationGroup = GameObject.FindGameObjectWithTag("AmmunationGroup").transform;
        GameObject shot =Instantiate(ammunition,  areaAttack.position, areaAttack.rotation, ammunationGroup);
        player.GetComponent<PlayerMoviment>().isAttack = false;
        shot.GetComponent<Shoot>()._playerId = _playerHealth._idPlayer;
        shot.GetComponent<Shoot>().player = areaAttack.forward;
        shot.GetComponent<Shoot>().playerRot = player.transform.eulerAngles;
    }
}