using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject emptySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    [SerializeField] private Image useButtonDisabled;
    [SerializeField] private GameObject discardButton;

    public ItemInventory currentItem;

    public void SetItemData(string description, bool consumable, bool unique)
    {
        descriptionText.text = description;
        if (consumable && unique)
        {
            useButton.SetActive(true);
            useButtonDisabled.enabled = false;
            discardButton.SetActive(false);
            Debug.Log("lol");
        }
        else if (!consumable && unique)
        {
            useButton.SetActive(false);
            useButtonDisabled.enabled = true;
            discardButton.SetActive(false);
            Debug.Log("lmao");
        }
        else if (consumable && !unique)
        {
            useButton.SetActive(true);
            useButtonDisabled.enabled = false;
            discardButton.SetActive(true);
        }
        else if(!consumable && !unique)
        {
            useButton.SetActive(false);
            useButtonDisabled.enabled = true;
            discardButton.SetActive(true);
        }
    }

    void CreateSlot()
    {
        if (playerInventory)
        {
            for(int i = 0; i < playerInventory.thisInventory.Count; i++)
            {
                if (playerInventory.thisInventory[i].itemCount > 0)
                {
                    GameObject temp = Instantiate(emptySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.thisInventory[i], this);
                    }
                }
            }
        }
    }

    // Start is called before the first frame update

    void OnEnable()
    {
        ClearSlots();
        CreateSlot();
        SetItemData("", false, true);
    }

    public void DisplayItemData(string itemDescriptionString, bool isConsumable, bool isUnique, ItemInventory newItem)
    {
        currentItem = newItem;
        descriptionText.text = itemDescriptionString;
        useButton.SetActive(isConsumable);
        useButtonDisabled.enabled = !isConsumable;
        discardButton.SetActive(!isUnique);
    }

    public void ClearSlots()
    {
        for(int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UseButtonClicked()
    {
        currentItem.UseItem();
        ClearSlots();
        CreateSlot();
        SetItemData("", false, true);
    }

    public void DiscardButtonClicked()
    {
        currentItem.DiscardItem();
        ClearSlots();
        CreateSlot();
        SetItemData("", false, true);
    }

    public void ResetItemCount()
    {
        for (int i = 0; i < playerInventory.thisInventory.Count; i++)
        {
            if (playerInventory.thisInventory[i].itemCount > 0)
            {
                playerInventory.thisInventory[i].itemCount = 0;
                Debug.Log("Clear all");
            }
        }
    }
}
