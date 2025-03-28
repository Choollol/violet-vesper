using System.Collections.Generic;
using UnityEngine;

public class PuzzleCounter : MonoBehaviour
{
    [SerializeField] private int totalPuzzlePieceCount;
    private int remainingPuzzlePieces;

    [SerializeField] private List<string> puzzleCompleteEventsToTrigger;

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.PuzzlePiecePlaced, PuzzlePiecePlaced);
    }
    private void OnDisable()
    {   
        EventMessenger.StopListening(EventKey.PuzzlePiecePlaced, PuzzlePiecePlaced);
    }
    private void Start()
    {
        remainingPuzzlePieces = totalPuzzlePieceCount;
    }
    private void PuzzlePiecePlaced()
    {
        --remainingPuzzlePieces;
        if (remainingPuzzlePieces == 0)
        {
            PuzzleComplete();
        }
    }
    private void PuzzleComplete()
    {
        foreach (string ev in puzzleCompleteEventsToTrigger)
        {
            EventMessenger.TriggerEvent(ev);
        }
    }
}
