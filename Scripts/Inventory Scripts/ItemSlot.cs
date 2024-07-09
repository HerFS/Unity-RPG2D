using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //==========ITEM DATA=========//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;

    [SerializeField]
    private int maxNumberOfItems;

    //==========ITEM SLOT=========//
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    //====ITEM DESCRIPTION SLOT====//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    //=========weaponData=========//
    public string[] weaponName;
    public Sprite[] weaponSprite;
    public string[] weaponDescription;
    public bool[] isDrop;

    public string[] enemyName;
    public int enemyNum;


    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        // checked to see if the slot is already full
        if (isFull)
        {
            return quantity;
        }

        // Update ITEM TYPE
        this.itemType = itemType;

        // Update NAME
        this.itemName = itemName;

        // Update Sprite
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        // Update Description
        this.itemDescription = itemDescription;

        // Update Quantity
        this.quantity += quantity;

        // Update QUANTITY TEXT
        quantityText.enabled = true;

        if (this.quantity >= maxNumberOfItems)
        {
            isFull = true;

            // Return the LEFTOVERS
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;

            quantityText.text = this.quantity.ToString();

            return extraItems;
        }

        quantityText.text = this.quantity.ToString();

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if (thisItemSelected)
        {
            if (this.quantity > 0)
            {
                bool usable = inventoryManager.UseItem(itemName);
                if (usable)
                {
                    this.quantity -= 1;
                    quantityText.text = this.quantity.ToString();

                    if (this.quantity <= 0)
                    {
                        EmptySlot();
                    }
                }
            }

            for (int i = 0; i < enemyName.Length; ++i)
            {
                if (enemyName[i] == "Bird" && itemName == "Bird")
                {
                    enemyNum = 0;
                    break;
                }
                else if (enemyName[i] == "Bear" && itemName == "Bear")
                {
                    enemyNum = 1;
                    break;
                }
                else if (enemyName[i] == "Boar" && itemName == "Boar")
                {
                    enemyNum = 2;
                    break;
                }
                else if (enemyName[i] == "Camel" && itemName == "Camel")
                {
                    enemyNum = 3;
                    break;
                }
                else if (enemyName[i] == "Flower Deer" && itemName == "Flower Deer")
                {
                    enemyNum = 4;
                    break;
                }
                else if (enemyName[i] == "Deer" && itemName == "Deer")
                {
                    enemyNum = 5;
                    break;
                }
                else if (enemyName[i] == "Fox" && itemName == "Fox")
                {
                    enemyNum = 6;
                    break;
                }
                else if (enemyName[i] == "Rabbit" && itemName == "Rabbit")
                {
                    enemyNum = 7;
                    break;
                }
                else if (enemyName[i] == "Green Snake" && itemName == "Green Snake")
                {
                    enemyNum = 8;
                    break;
                }
                else if (enemyName[i] == "Brown Snake" && itemName == "Brown Snake")
                {
                    enemyNum = 9;
                    break;
                }
                else if (enemyName[i] == "Blue Snake" && itemName == "Blue Snake")
                {
                    enemyNum = 10;
                    break;
                }
                else if (enemyName[i] == "Wolf" && itemName == "Wolf")
                {
                    enemyNum = 11;
                    break;
                }

            }
            CollectionItem(enemyName[enemyNum]);
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
        isFull = false;
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;
        itemName = "";
        itemDescription = "";
        itemSprite = emptySprite;
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {
        if (this.quantity > 0)
        {
            GameObject itemToDrop = new GameObject(itemName);
            Item newItem = itemToDrop.AddComponent<Item>();
            newItem.quantity = 1;
            newItem.itemName = itemName;
            newItem.sprite = itemSprite;
            newItem.itemDescription = itemDescription;
            newItem.itemType = itemType;
            isFull = false;

            SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
            sr.sprite = itemSprite;
            sr.sortingOrder = 5;
            sr.sortingLayerName = "Item";

            itemToDrop.AddComponent<BoxCollider2D>();
            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0, -0.4f, 0);
            itemToDrop.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);


            this.quantity -= 1;
            quantityText.text = this.quantity.ToString();

            if (this.quantity <= 0)
            {
                EmptySlot();
            }
        }
    }

    public void CollectionItem(string enemyName)
    {
        if (itemName == enemyName && quantity == 20 && isDrop[0] == false)
        {
            GameObject itemToDrop = new GameObject(weaponName[0]);
            Item newItem = itemToDrop.AddComponent<Item>();
            newItem.quantity = 1;
            newItem.name = weaponName[enemyNum];
            newItem.itemName = weaponName[enemyNum];
            newItem.sprite = weaponSprite[enemyNum];
            newItem.itemDescription = weaponDescription[enemyNum];
            newItem.itemType = ItemType.weapon;

            SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
            sr.sprite = weaponSprite[enemyNum];
            sr.sortingOrder = 5;
            sr.sortingLayerName = "Item";

            itemToDrop.AddComponent<BoxCollider2D>();
            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1.0f, -0.4f, 0);
            itemToDrop.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            isDrop[0] = true;
        }
    }
}
