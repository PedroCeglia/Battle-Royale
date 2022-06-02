using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Present : MonoBehaviour
{
    [Header("Atributes")]
    public bool isBig;
    private int health;
    [SerializeField]
    private List<GameObject> itensList = new List<GameObject>();
    private GameObject ItemsGroup;
    private SpawnBigPresents _spawnBigPresents;

    // Start is called before the first frame update
    void Start()
    {
        ItemsGroup = GameObject.FindGameObjectWithTag("ItemsGroup");
        _spawnBigPresents = GameObject.FindGameObjectWithTag("SpawnBigPresent").GetComponent<SpawnBigPresents>();
        if (isBig)
        {
            health = 12;
        }
        else
        {
            health = 4;
        }
    }

    [PunRPC]
    public void AddDamage(int hit)
    {
        health -= hit;
        if(health <= 0)
        {
            //Random.
            if (isBig) 
            {
                SpawnBigPresents.Instance._hasBigPresent = false;
                GameObject item5 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up + Vector3.left * 3 + Vector3.back * 3, transform.rotation, ItemsGroup.transform);
                GameObject item6 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up + Vector3.forward * 3 + Vector3.right * 3, transform.rotation, ItemsGroup.transform);
                GameObject item7 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up + Vector3.forward * 3 + Vector3.left * 3, transform.rotation, ItemsGroup.transform);
                GameObject item8 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up + Vector3.right * 3 + Vector3.back * 3, transform.rotation, ItemsGroup.transform);
                GameObject item9 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up, transform.rotation, ItemsGroup.transform);
            }
            GameObject item1 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up + Vector3.left * 2, transform.rotation, ItemsGroup.transform);
            GameObject item2 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up + Vector3.back * 2, transform.rotation, ItemsGroup.transform);
            GameObject item3 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up + Vector3.forward * 2, transform.rotation, ItemsGroup.transform);
            GameObject item4 = Instantiate(itensList[Random.Range(0, 3)], transform.position + Vector3.up + Vector3.right * 2, transform.rotation, ItemsGroup.transform);
            Destroy(gameObject);
        }
    }
}
