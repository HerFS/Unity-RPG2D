using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    // SLOT APPEARANCE //
    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private TMP_Text slotName;

    [SerializeField]
    private Image playerDisplayImage;

    // SLOT DATA //
    [SerializeField]
    private ItemType itemType = new ItemType();

    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;

    // OTHER VARIABLES //
    private bool slotInUse;
    [SerializeField]
    public GameObject selectedShader;

    [SerializeField]
    public bool thisItemSelected;

    [SerializeField]
    private Sprite emptySprite;

    public SpriteRenderer inGameMainHandSR;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentSOLibrary>();
        inGameMainHandSR = GameObject.Find("Weapon").GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // On Left Click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
    }

    void OnLeftClick()
    {
        if (thisItemSelected && slotInUse)
        {
            UnEquipGear();
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
    }

    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription)
    {
        // If somthign is already equipped, send it back before re-writing the data for this slot
        if (slotInUse)
        {
            UnEquipGear();
        }

        // Update Image
        this.itemSprite = itemSprite;
        inGameMainHandSR.sprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        // Update Data
        this.itemName = itemName;
        this.itemDescription = itemDescription;

        // Update the Display Image
        playerDisplayImage.sprite = itemSprite;

        // Update Player Stats
        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; ++i)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].EquipItem();
            }
        }

        slotInUse = true;
        
    }

    public void UnEquipGear()
    {
        inventoryManager.DeselectAllSlots();

        inventoryManager.AddItem(itemName, 1, itemSprite, itemDescription, itemType);

        // Update Slot Image
        this.itemSprite = emptySprite;
        inGameMainHandSR.sprite = emptySprite;
        slotImage.sprite = this.emptySprite;
        slotName.enabled = true;
        slotInUse = false;
        playerDisplayImage.sprite = emptySprite;

        // Update Player Stats
        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; ++i)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].UnEquipItem();
            }
        }

        GameObject.Find("StatManager").GetComponent<PlayerStats>().TurnOffPreviewStats();
    }
}
