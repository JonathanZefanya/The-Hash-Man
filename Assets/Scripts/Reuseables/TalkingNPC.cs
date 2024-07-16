using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : Interactable
{
    [SerializeField] private TextValue dialogueValue;
    [SerializeField] private TextAsset thisDialogue;
    [SerializeField] private Signals branchingDialogueSignal;

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("interact"))
            {
                dialogueValue.value = thisDialogue;
                branchingDialogueSignal.Raise();
            }
        }
    }
}
