using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField]
    private Text PlayerHealth;
    [SerializeField]
    private Image HealthBar;

    public void SetHelathInCanvas(float totalHealth, float atualHealth)
    {
        PlayerHealth.text = atualHealth.ToString();

        float fillAmountPorcent = atualHealth / (totalHealth / 100) / 100;
        HealthBar.fillAmount = fillAmountPorcent;
    }

}
