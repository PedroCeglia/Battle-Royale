using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private int smooth;
    [SerializeField]
    private float zP;
    [SerializeField]
    private float yP;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        smooth = 8;
        zP = 27.3f;
        yP = 35f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        // Set Z Position
        float camPz;
        if(player.position.z >= 578f)
        {
            camPz = 578f - zP;
        }
        else if (player.position.z <= 387.835)
        {
            camPz = 387.835f - zP;
        }
        else
        {
            camPz = player.position.z - zP;
        }

        // Set X Position
        float camPx;
        if(player.position.x >= 569.9717f)
        {
            camPx = 569.9717f;
        }
        else if (player.position.x <= 390.8779)
        {
            camPx = 390.8779f;
        }
        else
        {
            camPx = player.position.x;
        }

        // Set Cam Position And Add a Lerp(Smooth)
        Vector3 folling = new Vector3(camPx, yP, camPz);
        //Vector3 folling = new Vector3(player.position.x, yP, player.position.z -zP);
        transform.position = Vector3.Lerp(transform.position, folling, smooth * Time.deltaTime);
    }
}
