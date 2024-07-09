using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;

    private bool inventoryActivated;
    private bool equipmentActivated;
    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;
    public EquippedSlot[] equippedSlot;

    public ItemSO[] itemSOs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventoryActivated)
        {
            //Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            inventoryActivated = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !inventoryActivated)
        {
            //Time.timeScale = 0; // 멈추기
            InventoryMenu.SetActive(true);
            inventoryActivated = true;
            EquipmentMenu.SetActive(false);
            equipmentActivated = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && equipmentActivated)
        {
            //Time.timeScale = 1;
            EquipmentMenu.SetActive(false);
            equipmentActivated = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !equipmentActivated)
        {
            //Time.timeScale = 0; // 멈추기
            EquipmentMenu.SetActive(true);
            equipmentActivated = true;
            InventoryMenu.SetActive(false);
            inventoryActivated = false;
        }
    }

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; ++i)
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();

                return usable;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        if (itemType == ItemType.consumable || itemType == ItemType.crafting || itemType == ItemType.collectible)
        {
            for (int i = 0; i < itemSlot.Length; ++i)
            {
                if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);
                    }

                    return leftOverItems;
                }
            }
            return quantity;
        } else
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (equipmentSlot[i].isFull == false && equipmentSlot[i].itemName == itemName || equipmentSlot[i].quantity == 0)
                {
                    int leftOverItems = equipmentSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);
                    }

                    return leftOverItems;
                }
            }
            return quantity;
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }

        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            equipmentSlot[i].selectedShader.SetActive(false);
            equipmentSlot[i].thisItemSelected = false;
        }

        for (int i = 0; i < equippedSlot.Length; i++)
        {
            equippedSlot[i].selectedShader.SetActive(false);
            equippedSlot[i].thisItemSelected = false;
        }
    }
}

public enum ItemType
{
    consumable, // 소모품
    crafting, // 공예
    collectible, // 수집품
    weapon, // 무기
    defense, // 방어력
    none
};
