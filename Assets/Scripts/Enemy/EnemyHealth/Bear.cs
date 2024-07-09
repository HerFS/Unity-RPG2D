using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bear : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public string enemyName;
    public int enemyLevel;
    public int enemyEXP;

    public HealthBar healthBar;

    public GameObject healthBarImage;

    private LevelSystem levelSystem;
    public string[] itemName;
    public Sprite[] itemSprite;
    [TextArea]
    public string[] itemDescription;
    public TMP_Text enemyStat;

    private EnemySpawner spawnerBear; // 몬스터마다 수정


    private void Awake()
    {
        healthBarImage.SetActive(false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        spawnerBear = GameObject.FindWithTag("BearSpawner").GetComponent<EnemySpawner>();
        levelSystem = GameObject.Find("Player").GetComponent<LevelSystem>();
    }

    private void Start()
    {
        enemyStat.text = "LV." + enemyLevel.ToString() + " " + enemyName;
    }

    private void Update()
    {
        if (currentHealth <= 0) // fixed
        {
            if (gameObject.activeSelf == true)
            {
                currentHealth = maxHealth;
                healthBar.slider.value = currentHealth;
            }
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            healthBarImage.SetActive(true);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(WaitCoroutine());
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(3f);
        healthBarImage.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        healthBarImage.SetActive(true);
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public void Dead() // 죽었을 때 실행
    {
        int randomNum = Random.Range(1, 101);

        Destroy(gameObject);
        --spawnerBear.enemyCount;
        spawnerBear.timer = 0;
        if ((enemyLevel + 3) <= levelSystem.level)
        {
            levelSystem.GainExperienceFlateRate(enemyEXP / 2);
        }
        else if (enemyLevel >= levelSystem.level + 10)
        {
            levelSystem.GainExperienceFlateRate(enemyEXP / 2);
        }
        else
        {
            levelSystem.GainExperienceFlateRate(enemyEXP);
        }

        if (randomNum <= 30)
        {
            itemDrop1();
        }
        else if (randomNum <= 80)
        {
            itemDrop2();
        }
        else if (randomNum == 100)
        {
            itemDrop3();
        }
        else
        {

        }
    }

    void itemDrop1()
    {
        GameObject itemToDrop = new GameObject(itemName[0]);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.name = itemName[0];
        newItem.itemName = itemName[0];
        newItem.sprite = itemSprite[0];
        newItem.itemDescription = itemDescription[0];
        newItem.itemType = ItemType.consumable;

        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite[0];
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Item";

        itemToDrop.AddComponent<BoxCollider2D>();
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0.2f, -0.4f, 0);
        itemToDrop.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    void itemDrop2()
    {
        GameObject itemToDrop = new GameObject(itemName[0]);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.name = itemName[1];
        newItem.itemName = itemName[1];
        newItem.sprite = itemSprite[1];
        newItem.itemDescription = itemDescription[1];
        newItem.itemType = ItemType.collectible;

        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite[1];
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Item";

        itemToDrop.AddComponent<BoxCollider2D>();
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1.0f, -0.4f, 0);
        itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void itemDrop3()
    {
        GameObject itemToDrop = new GameObject(itemName[0]);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.name = itemName[1];
        newItem.itemName = itemName[1];
        newItem.sprite = itemSprite[1];
        newItem.itemDescription = itemDescription[1];
        newItem.itemType = ItemType.collectible;

        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite[1];
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Item";

        itemToDrop.AddComponent<BoxCollider2D>();
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1.0f, -0.4f, 0);
        itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
