using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<pot> pots = new List<pot>();
    public List<Interactable> interactables = new List<Interactable>();

    public GameObject virtualCamera;
    public TextMeshProUGUI placeNameText;
    public Collider2D roomBound;
    public Vector2 playerPos;
    public bool playerInBounds;
    [SerializeField] private float textDisplayTime = 2f;
    [SerializeField] private string placeName;

    protected void Start()
    {
        enemies = GetComponentsInChildren<Enemy>().Cast<Enemy>().ToList();
        pots = GetComponentsInChildren<pot>().Cast<pot>().ToList();
        interactables = GetComponentsInChildren<Interactable>().Cast<Interactable>().ToList();

        playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        if (roomBound.bounds.Contains(playerPos))
        {
            EnableChildren();
        }
        else
        {
            DisableChildren();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            EnableChildren();
            if(placeNameText != null)
            {
                StartCoroutine(AreaNameCo());
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            DisableChildren();
        }
    }

    protected virtual void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }

    protected virtual void DisableChildren()
    {
        foreach (Enemy enemy in enemies)
            ChangeActivation(enemy, false);

        foreach (pot pot in pots)
            ChangeActivation(pot, false);

        foreach (Interactable interactable in interactables)
            ChangeActivation(interactable, true);

        playerInBounds = false;
        virtualCamera.SetActive(false);
    }

    protected virtual void EnableChildren()
    {
        foreach (Enemy enemy in enemies)
            ChangeActivation(enemy, true);

        foreach (pot pot in pots)
            ChangeActivation(pot, true);

        foreach (Interactable interactable in interactables)
            ChangeActivation(interactable, true);

        playerInBounds = true;
        virtualCamera.SetActive(true);
    }

    private IEnumerator AreaNameCo()
    {
        if(placeName != null)
        {
            placeNameText.enabled = true;
            placeNameText.text = placeName;
            yield return new WaitForSeconds(textDisplayTime);
            placeNameText.enabled = false;
        }
        // This is for debugging
        else
        {
            placeNameText.enabled = true;
            placeNameText.text = "You forgor :skull_emoji:";
            yield return new WaitForSeconds(textDisplayTime);
            placeNameText.enabled = false;
        }
    }

}
