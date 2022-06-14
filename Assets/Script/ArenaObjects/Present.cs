using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Present : MonoBehaviourPunCallbacks
{
    [Header("Atributes")]
    public bool isBig;
    private int health;
    [SerializeField]
    private List<GameObject> itensList = new List<GameObject>();
    private GameObject ItemsGroup;

    [Header("Item List")]
    int[] _itemList1 = { 0, 1, 2, 3, 0, 1, 2, 3, 0};
    int[] _itemList2 = { 3, 1, 3, 1, 3, 0, 3, 2, 3};
    int[] _itemList3 = { 2, 2, 3, 2, 2, 1, 3, 1, 2};
    int[] _itemList4 = { 1, 3, 2, 1, 1, 3, 2, 1, 2};
    int[] _itemList5 = { 2, 2, 2, 3, 0, 2, 1, 2, 3};
    int[] _itemList6 = { 3, 1, 3, 2, 1, 1, 0, 3, 0};
    int[] _itemList7 = { 3, 1, 0, 3, 1, 0, 3, 0, 1 };
    int[] _itemList8 = { 2, 2, 1, 0, 2, 0, 2, 3, 0 };
    int[] _itemList9 = { 1, 3, 2, 2, 3, 0, 1, 2, 3 };
    int[] _itemList10 = { 0, 0, 1, 1, 3, 0, 1, 0, 1 };
    int[] _itemList11 = { 0, 2, 2, 2, 1, 0, 3, 1, 2 };
    int[] _itemList12 = { 0, 1, 3, 3, 2, 3, 2, 0, 1 };


    // Start is called before the first frame update
    void Start()
    {
        ItemsGroup = GameObject.FindGameObjectWithTag("ItemsGroup");
        if (isBig)
        {
            health = 12;
        }
        else
        {
            health = 4;
        }
    }

    public void CallAddDamage(object[] parms)
    {
        if(parms[0] != null && parms[1] != null)
        {
            photonView.RPC("AddDamage", RpcTarget.AllBuffered, parms);
        }
    }

    [PunRPC]
    private void AddDamage(object[] parms)
    {
        int hit = (int)parms[0];
        int indexItemChoice = (int)parms[1];

        health -= hit;
        if(health <= 0)
        {
            switch (indexItemChoice)
            {
                case 1:
                    CreateItens(_itemList1);
                    break;
                case 2:
                    CreateItens(_itemList2);
                    break;
                case 3:
                    CreateItens(_itemList3);
                    break;
                case 4:
                    CreateItens(_itemList4);
                    break;
                case 5:
                    CreateItens(_itemList5);
                    break;
                case 6:
                    CreateItens(_itemList6);
                    break;
                case 7:
                    CreateItens(_itemList7);
                    break;
                case 8:
                    CreateItens(_itemList8);
                    break;
                case 9:
                    CreateItens(_itemList9);
                    break;
                case 10:
                    CreateItens(_itemList10);
                    break;
                case 11:
                    CreateItens(_itemList11);
                    break;
                case 12:
                    CreateItens(_itemList12);
                    break;
            }
            Destroy(gameObject);
        }
    }
    private void CreateItens(int[] itensOrderListSpawn)
    {
        //Random.
        if (isBig)
        {
            GameController.Instance._hasBigPresent = false;
            GameObject item5 = Instantiate(itensList[itensOrderListSpawn[4]], transform.position + Vector3.up + Vector3.left * 3 + Vector3.back * 3, transform.rotation, ItemsGroup.transform);
            GameObject item6 = Instantiate(itensList[itensOrderListSpawn[5]], transform.position + Vector3.up + Vector3.forward * 3 + Vector3.right * 3, transform.rotation, ItemsGroup.transform);
            GameObject item7 = Instantiate(itensList[itensOrderListSpawn[6]], transform.position + Vector3.up + Vector3.forward * 3 + Vector3.left * 3, transform.rotation, ItemsGroup.transform);
            GameObject item8 = Instantiate(itensList[itensOrderListSpawn[7]], transform.position + Vector3.up + Vector3.right * 3 + Vector3.back * 3, transform.rotation, ItemsGroup.transform);
            GameObject item9 = Instantiate(itensList[itensOrderListSpawn[8]], transform.position + Vector3.up, transform.rotation, ItemsGroup.transform);
        }
        GameObject item1 = Instantiate(itensList[itensOrderListSpawn[0]], transform.position + Vector3.up + Vector3.left * 2, transform.rotation, ItemsGroup.transform);
        GameObject item2 = Instantiate(itensList[itensOrderListSpawn[1]], transform.position + Vector3.up + Vector3.back * 2, transform.rotation, ItemsGroup.transform);
        GameObject item3 = Instantiate(itensList[itensOrderListSpawn[2]], transform.position + Vector3.up + Vector3.forward * 2, transform.rotation, ItemsGroup.transform);
        GameObject item4 = Instantiate(itensList[itensOrderListSpawn[3]], transform.position + Vector3.up + Vector3.right * 2, transform.rotation, ItemsGroup.transform);
    }
}
