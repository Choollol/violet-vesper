using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData")]
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
        if (dialogueIndex >= dialogue.Count)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(dialogue[dialogueIndex].eventToTrigger))
        {
            EventMessenger.TriggerEvent(dialogue[dialogueIndex].eventToTrigger);
        }
        return dialogue[dialogueIndex++];
    }
}