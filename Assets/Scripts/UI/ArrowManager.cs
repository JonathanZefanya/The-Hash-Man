using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI arrowCountDisplay;

    // Start is called before the first frame update
    private void Start()
    {
        UpdateArrowCount();
    }

    public void UpdateArrowCount()
    {
        arrowCountDisplay.text = "" + playerInventory.arrow;  
    }

    public void GainArrow()
    {
        if(playerInventory.arrow < playerInventory.maxArrow)
        { 
            playerInventory.arrow += 1;
        }
    }

    public void SpendArrow()
    {
        playerInventory.arrow -= 1;
        UpdateArrowCount();
        if(playerInventory.arrow < 0)
        {
            playerInventory.arrow = 0;
        }
    }
}
