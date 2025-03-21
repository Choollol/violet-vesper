[System.Serializable]
public class DialogueNode
{
    public string speakerName;
    public string dialogueText;

    // Event to trigger after moving on from this node
    public string eventToTrigger;
}