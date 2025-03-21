using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private DialogueData currentDialogue = null;
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.BeginDialogue, BeginDialogue);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.BeginDialogue, BeginDialogue);
    }
    private void BeginDialogue()
    {
        currentDialogue = (DialogueData)DataMessenger.GetScriptableObject(ScriptableObjectKey.DialogueData);

        currentDialogue.BeginDialogue();
    }
}
