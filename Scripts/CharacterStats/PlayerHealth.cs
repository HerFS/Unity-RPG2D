using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public int levelUpHealth;
    private float lerpTimer;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.color = Color.yellow;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(health) + "/" + Mathf.Round(maxHealth);
    }

    public void pTakeDamage(int damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void IncreaseHealth() // 레벨에 따라 체력 증가
    {
        maxHealth += levelUpHealth;
        health = maxHealth;
    }
}
    