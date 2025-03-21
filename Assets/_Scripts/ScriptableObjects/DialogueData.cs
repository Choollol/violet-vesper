using System.Collections.Generic;
using UnityEngine;

public class DialogueData : ScriptableObject
{
    [SerializeField] private List<DialogueNode> dialogue;
    
    private int dialogueIndex;

    public void BeginDialogue()
    {
        dialogueIndex = 0;
    }

    /// <returns>The next dialogue in the list; null if end of dialogue is reached.</returns>
    public DialogueNode GetNextDialogue()
    {
        EventMessenger.TriggerEvent(dialogue[dialogueIndex].eventToTrigger);
        if (dialogueIndex < dialogue.Count)
        {
            return dialogue[dialogueIndex++];
        }
        return null;
    }
}