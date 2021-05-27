using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMana : MonoBehaviour
{
    public Image healthBar;
    public Image manaBar;
    public Text healthText;
    public Text manaText;

    public float maxHealth;
    public float maxMana;
    public float manaRegenSpeed;

    private float currentHealth;
    private float currentMana;
    private float calculateHealth;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
    void Update()
    {
        calculateHealth = currentHealth / maxHealth;
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, calculateHealth, Time.deltaTime);
        healthText.text = "" + (int)currentHealth;

        if(currentMana < maxMana)
        {
            manaBar.fillAmount = Mathf.MoveTowards(manaBar.fillAmount, 1f, Time.deltaTime * manaRegenSpeed);
            currentMana = Mathf.MoveTowards(currentMana / maxMana, 1f, Time.deltaTime * manaRegenSpeed) * maxMana;
        }

        if(currentMana < 0)
        {
            currentMana = 0;
        }
        manaText.text = "" + Mathf.FloorToInt(currentMana);
    }
    public void DamageTaken(float damage)
    {
        currentHealth -= damage;
    }
    public void ManaUsed(float mana)
    {
        if (mana <= currentMana)
        {
            currentMana -= mana;
            manaBar.fillAmount -= mana / maxMana;
        }
        else { Debug.Log("YOU HAVE NO MANA!"); }
    }
}
