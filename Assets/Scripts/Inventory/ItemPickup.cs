using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private ItemInventory thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
    }

    void AddItemToInventory()
    {
        if (playerInventory && thisItem)
        {
            if (playerInventory.thisInventory.Contains(thisItem))
            {
                thisItem.itemCount += 1;
            }
            else
            {
                playerInventory.thisInventory.Add(thisItem);
                thisItem.itemCount += 1;
            }
        }
    }
}
