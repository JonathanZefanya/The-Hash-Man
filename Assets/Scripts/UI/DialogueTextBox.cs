using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI thisText;

    public void Setup(string newDialogue)
    {
        thisText.text = newDialogue;
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
