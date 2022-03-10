using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Widgets")]
    private CharacterController controller;
    private Animator anim;
    public GameObject gun;

    [Header("Atributes")]
    private bool isWalk;
    public bool isAttack;
    [SerializeField]
    private int speed;
    private float turnSmoothVelocity;
    [SerializeField]
    private float smoothHotTime;
    private Vector3 moveDirection;
    public int hitForce;
    private int gravity;
    [SerializeField]
    private int TotalHealth;
    [SerializeField]
    private int health;

    [Header("Player Canvas")]
    public GameObject PlayerCanvas;


    // Start is called before the first frame update
    void Start()
    {
        // Config Attributes
        health = TotalHealth;
        hitForce = 1;

        // Start Widgets
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        // Config Canvas
        SetCanvas();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        FixedRotationCanvas();
    }
    // Move Player 
    private void Move()
    {
        if (controller.isGrounded)
        {
            // Get Moviment By Input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 moviment = new Vector3(horizontal, 0f, vertical);

            // Verify if moviment is != null && if Attack is null
            if(moviment.magnitude != 0 && !isAttack) 
            {
                anim.SetBool("isWalk", true);

                // Player Rotation
                float angle = Mathf.Atan2(moviment.x, moviment.z) * Mathf.Rad2Deg;//+ cam.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, smoothHotTime);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                // Camera Rotation
                moveDirection = Quaternion.Euler(0f, smoothAngle, 0f) * Vector3.forward * speed;

            }
            else if (!isAttack)
            {
                // if Player ins´t Moves 
                anim.SetBool("isWalk", false);
                moveDirection = Vector3.zero;
            }
            else
            {
                // if Player is Attacking he isn´t move
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            // If the Player isn´t touch the ground, he will fall
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move Player
        controller.Move(moveDirection * Time.deltaTime);

    }
    // Set Health
    public void SetHealth(int x)
    {
        // Verify if health + x is greater than TotalHealth
        // and health is equals TotalHealth
        // or x is lesser than 0
        if ((TotalHealth != health && TotalHealth > health + x) || x < 0) 
        { 
            health += x;
        } else if(TotalHealth < health + x)
        {
            health = TotalHealth;
        }
        // Config In Canvas
        SetCanvas();
        
        if(x < 0)
        {
            anim.SetTrigger("isHit");
        }
        if (health <= 0)
        {
            anim.SetTrigger("isDead");
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

    // Set HitForce
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
        PlayerCanvas.transform.Rotate(0f, 180f ,0);
    }
}
