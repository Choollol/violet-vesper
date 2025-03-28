using System.Collections;
using UnityEngine;

public class DialogueText : TMPUtil
{
    private const float defaultUnrollSpeed = 20f; // 1 = 1 character per second
    private const float slowUnrollSpeed = 5; // Speed to unroll at when slowed
    
    // Seconds to wait after finishing unrolling to prevent accidentally continuing too early
    private const float postUnrollWaitTime = 0.2f; 

    private const string slowUnrollChars = ".";

    protected override void StartListeningForEvents()
    {
        base.StartListeningForEvents();

        EventMessenger.StartListening(EventKey.UpdateDialogueText, UpdateDialogue);
        EventMessenger.StartListening(EventKey.FinishUnrollingDialogue, FinishUnrolling);
    }
    protected override void StopListeningToEvents()
    {
        base.StopListeningToEvents();

        EventMessenger.StopListening(EventKey.UpdateDialogueText, UpdateDialogue);
        EventMessenger.StopListening(EventKey.FinishUnrollingDialogue, FinishUnrolling);
    }
    private void UpdateDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(UnrollDialogue());
    }

    /// <summary>
    /// Unroll dialogue text
    /// </summary>
    private IEnumerator UnrollDialogue()
    {
        DataMessenger.SetBool(BoolKey.IsDialogueUnrolling, true);

        // Reset the TMPUGUI component's text
        text.text = "";

        string dialogueText = DataMessenger.GetString(StringKey.DialogueText);

        // Unroll text
        int index = 0;
        int textLength = dialogueText.Length;
        while (index < textLength)
        {
            char c = dialogueText[index++];
            text.text += c;
            yield return new WaitForSeconds(1 / (slowUnrollChars.Contains(c) ? slowUnrollSpeed : defaultUnrollSpeed));
        }

        yield return new WaitForSeconds(postUnrollWaitTime);

        FinishUnrolling();
        yield break;
    }

    /// <summary>
    /// Cleans up after dialogue is unrolled
    /// </summary>
    private void FinishUnrolling()
    {
        StopAllCoroutines();
        text.text = DataMessenger.GetString(StringKey.DialogueText);
        DataMessenger.SetBool(BoolKey.IsDialogueUnrolling, false);
        //EventMessenger.TriggerEvent(EventKey.DialogueUnrolled);
    }
}