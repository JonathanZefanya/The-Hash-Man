using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Chest contents")]
    public Item contents;
    public Inventory playerInventory;
    [SerializeField] private ItemPickup thisItem;
    public LootTable itemLoot;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signals and Messages")]
    public Signals raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Animation")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.runtimeValue;
        if (isOpen)
        {
            anim.SetBool("opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            if(!isOpen)
            {
                // Open the chest
                OpenChest();
            }
            else
            {
                // Chest already opened
                ChestOpened();
            }
        }
    }

    public void OpenChest()
    {
        // Dialog window on
        dialogBox.SetActive(true);
        // dialog text = content texts
        dialogText.text = contents.itemDescription;
        // add content to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        if(itemLoot != null)
        {
            ItemPickup now = itemLoot.ItemLoot();
            if (now != null)
            {
                Instantiate(now.gameObject, transform.position, Quaternion.identity);
            }
        }
        // raise signal to player to animate
        raiseItem.Raise();
        // raise the context clue
        contextOff.Raise();
        // set the chest to opened
        isOpen = true;
        anim.SetBool("opened", true);
        storedOpen.runtimeValue = isOpen;
    }

    public void ChestOpened()
    {
        // Dialog off
        dialogBox.SetActive(false);
        // raise signal to player to stop animating
        raiseItem.Raise();
        anim.SetBool("opened", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }
}
