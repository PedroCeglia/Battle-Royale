using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPunCallbacks
{
    [Header("Widgets")]
    private Animator anim;

    [Header("Atributes")]
    [SerializeField] private int TotalHealth;
    [SerializeField] private int health;
    [SerializeField] private int hitForce;

    [Header("Player Canvas")]
    public GameObject PlayerCanvas;

    [Header("WallZone")]
    public bool inWall;
    public bool inWallDamage;
    [SerializeField] [Range(1, 10)] private float _inWallDamageTime;
    [SerializeField] [Range(1, 10)] private int _inWallDamagePower;

    [Header("GameOver UI")] 
    private GameOverMenu _gameOverMenu; 


    // Photon Player
    public int _idPlayer { get; private set; }

     
    // Start is called before the first frame update
    void Start()
    {
        // Config Attributes
        health = TotalHealth;
        inWall = true;
        inWallDamage = false;
        if (photonView.IsMine)
        {
            // Get Id Player
            _idPlayer = PhotonNetwork.LocalPlayer.ActorNumber;
            GameController.Instance._playerLogMineId = _idPlayer;
            // Get Menu GameOver
            _gameOverMenu = GameController.Instance._gameOverMenu;
        }
        
        // Start Widgets
        anim = GetComponent<Animator>();

        // Config Canvas
        SetCanvas();
    }

    // Verify if player is inWall asnd Active DamagePlayer Coroutine
    private void Update()
    {
        if (!inWall)
        {
            StartCoroutine("DamagePlayer");
        }
    }

    // Damage Player
    IEnumerator DamagePlayer()
    {
        if (!inWall && !inWallDamage)
        {
            inWallDamage = true;
            yield return new WaitForSeconds(_inWallDamageTime);
            SetHealth(-_inWallDamagePower);
            inWallDamage = false;
        }
    }

    private void LateUpdate()
    {
        FixedRotationCanvas();
    }

    // Set Health
    public void SetHealth(int x)
    {
        if(health > 0)
        {
            // Verify if health + x is greater than TotalHealth
            // and health is equals TotalHealth
            // or x is lesser than 0
            if ((TotalHealth != health && TotalHealth > health + x) || x < 0)
            {
                health += x;
            }
            else if (TotalHealth < health + x)
            {
                health = TotalHealth;
            }
            // Config In Canvas
            SetCanvas();

            // Config Animations
            if (x < 0 && health >= 0)
            {
                anim.SetTrigger("isHit");
            }
            if (health <= 0)
            {
                anim.SetBool("isDead", true);

                // Game Over Config

                // Open GameOver Menu
                if (photonView.IsMine)
                {
                    Debug.Log("ooioio");
                    GameController.Instance._gameOverMenu.SetMenuActive(true);
                }
               
                // Destroy Player
                Destroy(gameObject, 1.2f);
            }
        }
    }

    // Set TotalHealth
    public void SetTotalHealth(int x)
    {
        TotalHealth += x;
        health += x;

        // Config In Canvas
        SetCanvas();
    }

    // Set HitForce  // Mexer Pois Nao Faz nada !!!
    public void SetHitForce(int x)
    {
        hitForce += x;
    }

    // Set Canvas
    private void SetCanvas()
    {
        gameObject.GetComponent<PlayerCanvas>().SetHelathInCanvas(TotalHealth, health);
    }

    // Set Canvas Position
    private void FixedRotationCanvas()
    {
        PlayerCanvas.transform.LookAt(Camera.main.transform);
        PlayerCanvas.transform.Rotate(0f, 180f, 0);
    }

}


