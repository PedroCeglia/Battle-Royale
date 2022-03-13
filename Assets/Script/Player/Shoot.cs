using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Rigidbody rig;
    public int speed;
    public int hitPower;

    public Vector3 player;
    public Vector3 playerRot;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        speed = 100;
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
            collision.gameObject.GetComponent<Present>().AddDamage(1);
            Destroy(gameObject, 0.05f);
        }
    }
}
