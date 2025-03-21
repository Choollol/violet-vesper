using System.Collections.Generic;
using UnityEngine;

public class EventTriggerInteractable : ButtonUtil
{
    [SerializeField] private List<string> eventsToTrigger;
    protected override void HandleClick()
    {
        base.HandleClick();

        foreach (string eventName in eventsToTrigger)
        {
            EventMessenger.TriggerEvent(eventName);
        }
    }
}
