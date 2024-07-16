using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private TextMeshProUGUI itemCountDisplay;
    [SerializeField] private Image itemSprite;

    [Header("Item variables")]
    public ItemInventory thisItem;
    public InventoryManager inventoryManager;
    
    public void Setup(ItemInventory newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        inventoryManager = newManager;
        if (thisItem)
        {
            itemSprite.sprite = thisItem.itemSprite;
            itemCountDisplay.text = "" + thisItem.itemCount;
        }
    }

    public void InspectItem()
    {
        if (thisItem)
        {
            inventoryManager.DisplayItemData(thisItem.itemDescription, thisItem.consumable, thisItem.unique, thisItem);
        }
    }
}
