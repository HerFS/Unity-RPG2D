using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public int maxLevel;
    public float currentXp;
    public float requiredXp;
    public int levelUpAttack;

    private float lerpTimer;
    private float delayTimer;
    [Header("UI")]
    public Image frontXpBar;

    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        frontXpBar.fillAmount = currentXp / requiredXp;
        requiredXp = CalculateRequiredXp();
        levelText.text = level.ToString();

        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();

        if (currentXp > requiredXp && level < maxLevel)
        {
            LevelUp();
        } else if (level == maxLevel)
        {
            levelText.text = "MAX";
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = frontXpBar.fillAmount;
        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            frontXpBar.fillAmount = xpFraction;
        }
        xpText.text = Mathf.Round(currentXp) + "/" + Mathf.Round(requiredXp);
    }

    public void GainExperienceFlateRate(float xpGained) // 경험치 얻기
    {
        currentXp += xpGained;
        delayTimer = 0f;
    }

    public void LevelUp() // 레벨업
    {
        ++level;
        playerStats.attack += levelUpAttack;
        frontXpBar.fillAmount = 0f;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        GetComponent<PlayerHealth>().IncreaseHealth();
        requiredXp = CalculateRequiredXp();
        levelText.text = level.ToString();
    }

    public int CalculateRequiredXp() // 레벨업 경험치 설정
    {
        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; ++levelCycle)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }

        return solveForRequiredXp / 4;
    }
}
