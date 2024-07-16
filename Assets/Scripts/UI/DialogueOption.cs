using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI thisText;
    private int optionValue;
    public void Setup(string newDialogue, int thisOption)
    {
        thisText.text = newDialogue;
        optionValue = thisOption;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
