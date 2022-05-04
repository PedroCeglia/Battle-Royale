using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerMoviment : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Widgets")]
    private CharacterController controller;
    private Animator anim;

    [Header("Atributes")]
    public bool isAttack = false;
    [SerializeField]
    private int speed;
    private float turnSmoothVelocity;
    [SerializeField]
    private float smoothHotTime;
    private Vector3 moveDirection;
    private int gravity;

    // Player Photon
    public bool isMine { get; private set; } = false;
    private Player _photonPlayer;
    private int _id;

    // Lag
    private float lag;
    private Vector3 networkPosition;

    [PunRPC]
    private void Initialize(Player player)
    {
        _photonPlayer = player;
        _id = _photonPlayer.ActorNumber;
        GameController.Instance._jogadores.Add(this);
        if (photonView.IsMine)
        {
            isMine = true;   
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start Widgets
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(lag);
        if (isMine)
        {
            Move();
        }
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
            if (moviment.magnitude != 0 && !isAttack)
            {
                anim.SetBool("isWalk", true);

                // Player Rotation
                float angle = Mathf.Atan2(moviment.x, moviment.z) * Mathf.Rad2Deg;//+ cam.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, smoothHotTime);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                // Camera Rotation
                moveDirection = Quaternion.Euler(0f, smoothAngle, 0f) * Vector3.forward * speed;
                Debug.Log(moveDirection);

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
        if(lag > 0)
        {
            controller.Move(networkPosition * Time.deltaTime);
        }
        else
        {
            controller.Move(moveDirection * Time.deltaTime);
        }
        
    }

    [System.Obsolete]
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(moveDirection * Time.deltaTime);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            Quaternion networkRotation = (Quaternion)stream.ReceiveNext();
            moveDirection = (Vector3)stream.ReceiveNext();

            lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
            networkPosition += (moveDirection * lag);
        }
    }
}
