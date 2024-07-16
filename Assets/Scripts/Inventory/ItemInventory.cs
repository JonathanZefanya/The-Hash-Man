using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
[System.Serializable]
public class ItemInventory : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemCount;
    public TextMeshProUGUI itemCountText;
    public bool consumable;
    public bool unique;
    public UnityEvent thisEvent;

    public void UseItem()
    {
        thisEvent.Invoke();
    }

    public void DiscardItem()
    {
        UpdateItemCount(1);
    }
    public void UpdateItemCount(int amountDecreased)
    {
        itemCount--;
        if(itemCount < 0)
        {
            itemCount = 0;
        }
    }
}
