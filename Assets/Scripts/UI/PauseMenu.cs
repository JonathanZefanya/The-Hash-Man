using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    private bool inventoryOpened;
    private bool deathScreen;
    public GameObject pausePanel;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject gameSaveManager;
    [SerializeField] private GameObject deathOverlay;
    public string titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen = false;
        isPaused = false;
        inventoryOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathScreen)
        {
            deathOverlay.SetActive(true);
        }
        else
        {
            deathOverlay.SetActive(false);
            if (Input.GetButtonDown("pause"))
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    pausePanel.SetActive(true);
                    inventoryOpened = false;
                    inventoryUI.SetActive(false);
                    Time.timeScale = 0f;
                }
                else
                {
                    pausePanel.SetActive(false);
                    Time.timeScale = 1f;
                }
            }

            if (isPaused == false)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f;

                if (Input.GetButtonDown("inventory"))
                {
                    DisplayInventorySwitch();
                }
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene(titleScreen);
    }


    public void DisplayInventorySwitch()
    {
        if (!deathScreen)
        {
            inventoryOpened = !inventoryOpened;
            if (inventoryOpened)
            {
                inventoryUI.SetActive(true);
            }
            else
            {
                inventoryUI.SetActive(false);
            }
        }
    }

    public void DeathScreen()
    {
        deathScreen = true;
        Time.timeScale = 0f;
    }

    public void AliveScreen()
    {
        deathScreen = false;
        Time.timeScale = 1f;
    }
}
