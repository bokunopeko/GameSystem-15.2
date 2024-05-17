using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;

    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Color staminaColor;

    void Start()
    {
        //image . fillamount is between 0 to 1
        healthBar.fillAmount = 1;
        staminaBar.fillAmount = 1;

        staminaBar.color = staminaColor;

        //gradint.evaluate(flat) - return the colour at the gragient position , 0 and 1
        healthBar.color = healthGradient.Evaluate(1);
    }

    public void UpdateHUD(float healthPercent, float staminaPercent)
    {
        healthBar.fillAmount = healthPercent;
        healthBar.color = healthGradient.Evaluate((float)healthPercent);
        staminaBar.fillAmount = staminaPercent;

    }

}
