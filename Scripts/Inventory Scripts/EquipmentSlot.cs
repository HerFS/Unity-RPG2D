using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    //==========ITEM DATA=========//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;

    //==========ITEM SLOT=========//
    [SerializeField]
    private Image itemImage;


    //=======EQUIPPED SLOTS======//
    [SerializeField]
    private EquippedSlot weaponSlot;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentSOLibrary>();
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
        this.quantity = 1;
        isFull = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
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
        if (thisItemSelected && quantity == 1)
        {
            EquipGear();
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; ++i)
            {
                if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                {
                    equipmentSOLibrary.equipmentSO[i].PreviewEquipment();
                }
            }
        }

        if (!isFull)
        {
            GameObject.Find("StatManager").GetComponent<PlayerStats>().TurnOffPreviewStats();
        }

    }

    private void EquipGear()
    {
        if (itemType == ItemType.weapon)
        {
            weaponSlot.EquipGear(itemSprite, itemName, itemDescription);
        }

        EmptySlot();
    }

    private void EmptySlot()
    {
        itemImage.sprite = emptySprite;
        itemName = "";
        itemDescription = "";
        itemSprite = emptySprite;
        quantity = 0;
        isFull = false;
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

            SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
            sr.sprite = itemSprite;
            sr.sortingOrder = 5;
            sr.sortingLayerName = "Item";

            itemToDrop.AddComponent<BoxCollider2D>();
            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0, -0.4f, 0);
            itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            this.quantity -= 1;

            if (this.quantity <= 0)
            {
                EmptySlot();
            }
        }
    }
}
