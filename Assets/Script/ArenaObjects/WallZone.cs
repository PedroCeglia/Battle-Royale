using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallZone : MonoBehaviour
{
    [Header("Wall Zone Atributes")]
    [SerializeField]
    private float xRadius;
    [SerializeField]
    private float yHeight;
    [SerializeField]
    private float DecreaseSpeed;
    [SerializeField]
    private float DecreaseWaitTime;
    [SerializeField]
    private bool Shrinking;
    [SerializeField]
    private bool isDecrease;

    [Header("Player in Wall Zone")]
    [SerializeField]
    private float PlayerDamageTime;
    [SerializeField]
    private int DamagePower;
    [SerializeField]
    private bool inWall = true;
    [SerializeField]
    private bool isDamage;
    private GameObject player;

    [Header("Safe Zone Atributes")]
    [SerializeField]
    private float xRadiusSafe;
    [SerializeField]
    private float yHeightSafe;
    private GameObject SafeZone;

    // Start is called before the first frame update
    void Start()
    {
        SafeZone = GameObject.FindGameObjectWithTag("SafeZone");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(xRadius, yHeight, xRadius);
        if (!inWall)
        {
            StartCoroutine("DamagePlayer", player );
        }
        StartCoroutine("DecreaseTime");
    }

    // If Player Get Out the Collider
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            player = coll.gameObject;
            inWall = false;
        }
    }
    // If Player Enter the Collider
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (!inWall)
            {
                StopCoroutine("DamagePlayer");
                inWall = true;
                isDamage = false;
            }
        }
    }

    // Damage Player
    IEnumerator DamagePlayer(GameObject player)
    {
        if (!inWall && !isDamage)
        {
            isDamage = true;
            yield return new WaitForSeconds(PlayerDamageTime);
            player.GetComponent<PlayerHealth>().SetHealth(-DamagePower);
            isDamage = false;
        }
    }

    // Timer Decrease the Wall Zone
    IEnumerator DecreaseTime()
    {
        if (!isDecrease)
        {
            isDecrease = true;
            yield return new WaitForSeconds(DecreaseWaitTime);
            DecreaseWallZone();
        } 
        else if (Shrinking)
        {
            if (xRadius > xRadiusSafe)
            {
                xRadius -= DecreaseSpeed * Time.deltaTime;
            }
            else
            {
                Shrinking = false;
                if (isDecrease)
                {
                    isDecrease = false;
                }
            }
        }
    }

    // Decrease the Wall Zone
    private void DecreaseWallZone()
    {
        if (!Shrinking) 
        {
            if(xRadiusSafe >= 0.3)
            {
                xRadiusSafe -= 0.2f;
            }
            SafeZone.transform.localScale = new Vector3(xRadiusSafe, yHeightSafe, xRadiusSafe);
            Shrinking = true;
        }
    }
}
