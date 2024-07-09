using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int attack;
    [SerializeField]
    private TMP_Text attackText;
    [SerializeField]
    private TMP_Text healthText;

    private PlayerHealth pHealth;

    [SerializeField]
    private TMP_Text attackPreText;
    [SerializeField]
    private Image previewImage;

    [SerializeField]
    private GameObject selectedItemStats;
    [SerializeField]
    private GameObject selectedItemImage;

    // Start is called before the first frame update
    private void Start()
    {
        pHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        UpdateEquipmentStats();
    }

    private void Update()
    {
        healthText.text = Mathf.Round(pHealth.health).ToString() + "/" + Mathf.Round(pHealth.maxHealth).ToString();
        attackText.text = attack.ToString();
    }

    public void UpdateEquipmentStats()
    {
        attackText.text = attack.ToString();
    }

    public void PreviewEquipmentStats(int attack, Sprite itemSprite)
    {
        attackPreText.text = attack.ToString();

        previewImage.sprite = itemSprite;
        selectedItemStats.SetActive(true);
        selectedItemImage.SetActive(true);
    }

    public void TurnOffPreviewStats()
    {
        selectedItemStats.SetActive(false);
        selectedItemImage.SetActive(false);
    }
}
