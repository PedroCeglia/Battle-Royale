using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Widgets")]
    private Animator anim;

    [Header("Atributes")]
    public int hitForce;
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
        anim = GetComponent<Animator>();

        // Config Canvas
        SetCanvas();
    }


    private void LateUpdate()
    {
        FixedRotationCanvas();
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
        }
        else if (TotalHealth < health + x)
        {
            health = TotalHealth;
        }
        // Config In Canvas
        SetCanvas();

        if (x < 0)
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
        PlayerCanvas.transform.Rotate(0f, 180f, 0);
    }
}


