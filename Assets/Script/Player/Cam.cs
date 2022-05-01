using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Cam : MonoBehaviourPunCallbacks
{
    private List<GameObject> players = new List<GameObject>();
    private Transform playerLocation;
    private bool _verifyAllUsers;
    [SerializeField]
    private int smooth;
    [SerializeField]
    private float zP;
    [SerializeField]
    private float yP;

    // Start is called before the first frame update
    void Start()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        smooth = 8;
        zP = 27.3f;
        yP = 35f;
    }

    // Get User Player
    void GetUserPlayer() {
        Debug.Log(players.Count);
        foreach(GameObject user in players)
        {
            PlayerMoviment player = user.GetComponent<PlayerMoviment>();
            if (player.isMine) {
                playerLocation = player.transform;
                _verifyAllUsers = true;
            }
        }
    }

    private void Update()
    {
        if (!_verifyAllUsers)
        {
            if(players.Count == 0) {
                players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            }else if (players.Count < PhotonNetwork.PlayerList.Length)
            {
                players.Clear();
                players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            }
            else if (players.Count == PhotonNetwork.PlayerList.Length)
            {
                GetUserPlayer();
                _verifyAllUsers = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerLocation != null)
        {
            Move();
        }
    }

    private void Move()
    {

        // Set Z Position
        float camPz;
        if(playerLocation.position.z >= 578f)
        {
            camPz = 578f - zP;
        }
        else if (playerLocation.position.z <= 387.835)
        {
            camPz = 387.835f - zP;
        }
        else
        {
            camPz = playerLocation.position.z - zP;
        }

        // Set X Position
        float camPx;
        if(playerLocation.position.x >= 569.9717f)
        {
            camPx = 569.9717f;
        }
        else if (playerLocation.position.x <= 390.8779)
        {
            camPx = 390.8779f;
        }
        else
        {
            camPx = playerLocation.position.x;
        }

        // Set Cam Position And Add a Lerp(Smooth)
        Vector3 folling = new Vector3(camPx, yP, camPz);
        transform.position = Vector3.Lerp(transform.position, folling, smooth * Time.deltaTime);
    }
}
