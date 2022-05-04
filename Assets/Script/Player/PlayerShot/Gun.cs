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
    private Animator playerAnim;
    private PlayerMoviment playerMoviment;

    [Header("Atrributes")]
    [SerializeField]
    [Range(1, 10)]
    private int damage;
    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float fireRate;
    [SerializeField]
    private float fireRange;
    [SerializeField]
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        playerMoviment = player.GetComponent<PlayerMoviment>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= fireRate)
        {
            if (playerMoviment.isMine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    photonView.RPC("Attack", RpcTarget.All);
                    timer = 0f;
                
                }
            }
        }
        else
        {
            player.GetComponent<PlayerMoviment>().isAttack = false;
            timer += Time.deltaTime;
        }
    }
    [PunRPC]
    public void Attack()
    {
        //
        playerAnim.SetTrigger("isAttack");
        player.GetComponent<PlayerMoviment>().isAttack = true;
        Transform ammunationGroup = GameObject.FindGameObjectWithTag("AmmunationGroup").transform;
        GameObject shot =Instantiate(ammunition,  areaAttack.position, areaAttack.rotation, ammunationGroup);
        shot.GetComponent<Shoot>().player = areaAttack.forward;
        shot.GetComponent<Shoot>().hitPower = player.GetComponent<PlayerHealth>().hitForce;
        shot.GetComponent<Shoot>().playerRot = player.transform.eulerAngles;
    }
}
// PhotonNetwork.Instantiate("AssaultRifleAmmunition", areaAttack.position, areaAttack.rotation); //