using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Widgets")]
    public GameObject ammunition;
    public Transform areaAttack;
    public GameObject player;
    private Animator playerAnim;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= fireRate)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                timer = 0f;
                
            }
        }
        else
        {
            player.GetComponent<Player>().isAttack = false;
            timer += Time.deltaTime;
        }
    }

    public void Attack()
    {
        playerAnim.SetTrigger("isAttack");
        player.GetComponent<Player>().isAttack = true;
        Transform ammunationGroup = GameObject.FindGameObjectWithTag("AmmunationGroup").transform;
        GameObject shot = Instantiate(ammunition,  areaAttack.position, areaAttack.rotation, ammunationGroup);
        shot.GetComponent<Shoot>().player = areaAttack.forward;
        shot.GetComponent<Shoot>().hitPower = player.GetComponent<Player>().hitForce;
        shot.GetComponent<Shoot>().playerRot = player.transform.eulerAngles;
    }
}
