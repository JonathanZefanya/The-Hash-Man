using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickUp : PowerUp
{
    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        powerUpSignal.Raise();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            if (playerInventory.arrow < playerInventory.maxArrow)
            {
                playerInventory.arrow += 1;
                powerUpSignal.Raise();
                Destroy(this.gameObject);
            }
        }
    }
}
