using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePrefab;
    private GameObject dialogueInstance;

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

        dialogueInstance = Instantiate(dialoguePrefab);

        EventMessenger.StartListening(EventKey.Continue, Continue);

        Continue();
    }
    private void Continue()
    {
        if (DataMessenger.GetBool(BoolKey.IsDialogueUnrolling))
        {
            EventMessenger.TriggerEvent(EventKey.FinishUnrollingDialogue);
        }
        else
        {
            DialogueNode node = currentDialogue.GetNextDialogue();
            if (node == null)
            {
                EndDialogue();
                return;
            }

            DataMessenger.SetString(StringKey.DialogueText, node.dialogueText);
            DataMessenger.SetString(StringKey.DialogueSpeakerName, node.speakerName + ':');

            EventMessenger.TriggerEvent(EventKey.UpdateDialogueText);
            EventMessenger.TriggerEvent(EventKey.UpdateDialogueSpeakerName);
        }
    }
    private void EndDialogue()
    {
        Destroy(dialogueInstance);

        EventMessenger.StopListening(EventKey.Continue, Continue);
    }
}
