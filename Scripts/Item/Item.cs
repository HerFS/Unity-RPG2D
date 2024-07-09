using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;
    [SerializeField]
    public int quantity;
    [SerializeField]
    public Sprite sprite;
    [TextArea]
    [SerializeField]
    public string itemDescription;

    private InventoryManager inventoryManager;

    public ItemType itemType;

    private BoxCollider2D myCollider;
    private float timer;

    // Start is called before the first frame update
    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.isTrigger = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 180f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.Z))
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, itemType);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }
}
