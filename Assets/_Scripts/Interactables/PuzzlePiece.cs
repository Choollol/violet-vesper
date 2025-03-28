using UnityEngine;

public class PuzzlePiece : DragAndDropInteractable
{
    protected override void SnapToDestination()
    {
        base.SnapToDestination();

        EventMessenger.TriggerEvent(EventKey.PuzzlePiecePlaced);
    }
}
