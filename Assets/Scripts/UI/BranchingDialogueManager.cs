using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class BranchingDialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameObject dialoguePrefab;
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private TextValue dialogueValue;
    [SerializeField] private Story thisStory;
    [SerializeField] private GameObject dialogueBackground;
    [SerializeField] private GameObject optionBackground;
    [SerializeField] private ScrollRect dialogueScroll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        dialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    public void EnableCanvas()
    {
        dialogueCanvas.SetActive(true);
        SetStory();
        RefreshView();
    }

    public void SetStory()
    {
        if (dialogueValue.value)
        {
            DeleteOldDialogues();
            thisStory = new Story(dialogueValue.value.text);
        }
        else
        {
            Debug.Log("no story available");
        }
    }

    void DeleteOldDialogues()
    {
        for(int i = 0; i < dialogueBackground.transform.childCount; i++)
        {
            Destroy(dialogueBackground.transform.GetChild(i).gameObject);
        }
    }

    public void RefreshView()
    {
        while (thisStory.canContinue)
        {
            CreateNewDialogue(thisStory.Continue());
        }
        if(thisStory.currentChoices.Count > 0)
        {
            CreateNewOptions();
        }
        else
        {
            dialogueCanvas.SetActive(false);
        }
        StartCoroutine(ScrollCo());
    }

    IEnumerator ScrollCo()
    {
        // Set dialogue scroll to always be at the bottom after waiting one frame
        yield return null;
        dialogueScroll.verticalNormalizedPosition = 0f;
    }

    public void CreateNewDialogue(string newDialogue)
    {
        DialogueTextBox newDialogueBox = Instantiate(dialoguePrefab, dialogueBackground.transform).GetComponent<DialogueTextBox>();
        newDialogueBox.Setup(newDialogue);
    }

    public void CreateNewResponse(string newDialogue, int optionValue)
    {
        DialogueOption newDialogueOption = Instantiate(optionPrefab, optionBackground.transform).GetComponent<DialogueOption>();
        newDialogueOption.Setup(newDialogue, optionValue);
        Button responseButton = newDialogueOption.gameObject.GetComponent<Button>();
        if (responseButton)
        {
            responseButton.onClick.AddListener(delegate { ChooseOption(optionValue); });
        }
    }

    public void CreateNewOptions()
    {
        for(int i = 0; i < optionBackground.transform.childCount; i++)
        {
            Destroy(optionBackground.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < thisStory.currentChoices.Count; i++)
        {
            CreateNewResponse(thisStory.currentChoices[i].text, i);
        }
    }

    void ChooseOption(int option)
    {
        thisStory.ChooseChoiceIndex(option);
        RefreshView();
    }
}
