using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviour
{
    private Rigidbody rig;
    [Header("Attributes")]
    [SerializeField] [Range(50, 250)] private int speed;
    [SerializeField] [Range(1,10)] private int hitPower;
    public int _playerId;

    public Vector3 player;
    public Vector3 playerRot;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(90f, playerRot.y, 0f);
        Destroy(gameObject, 0.6f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.AddForce(player * speed * Time.deltaTime,ForceMode.Impulse);
    }

    // Shoot Collision
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 7)
        {
            if (_playerId == GameController.Instance._playerLogMineId)
            {
                object[] parms = new object[2] { 1, Random.Range(1,12)};

                collision.gameObject.GetComponent<Present>().CallAddDamage(parms);
            }
            Destroy(gameObject, 0.05f);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verify if the player that shoted is the player that collided
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (_playerId != player._idPlayer)
            {
                player.SetHealth(-hitPower);
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verify if the player that shoted is the player that collided
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            Debug.Log(player._idPlayer);
            Debug.Log(_playerId);
            if (_playerId != player._idPlayer)
            {
                player.SetHealth(-hitPower);
                Destroy(gameObject);
            }
        } 
    }
}
