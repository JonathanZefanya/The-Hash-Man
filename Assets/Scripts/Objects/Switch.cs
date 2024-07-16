using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer inactiveSprite;
    public Door thisDoor;

    // Start is called before the first frame update
    void Start()
    {
        inactiveSprite = GetComponent<SpriteRenderer>();
        active = storedValue.runtimeValue;
        if (active)
        {
            ActivateButton();
        }
    }

    public void ActivateButton()
    {
        active = true;
        storedValue.runtimeValue = active;
        thisDoor.Open();
        inactiveSprite.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateButton();
        }
    }
}
