using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField]
    private int ItemType;
    [SerializeField]
    private int rotationSpeed;
    [SerializeField]
    private int hitForceAdd;
    [SerializeField]
    private int TotalHealthAdd;
    [SerializeField]
    private int TotalHealing;
    [SerializeField]
    private int TotalSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.back * rotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Action(coll.gameObject);
            Destroy(gameObject, 0.1f);
        }
    }

    private void Action(GameObject player)
    {
        switch (ItemType)
        {
            case 0:
                player.GetComponent<Player>().SetHitForce(hitForceAdd);
                break;
            case 1:
                player.GetComponent<Player>().SetTotalHealth(TotalHealthAdd);
                break;
            case 3:
                player.GetComponent<Player>().SetHealth(TotalHealing);
                break;
        }
    }
}
