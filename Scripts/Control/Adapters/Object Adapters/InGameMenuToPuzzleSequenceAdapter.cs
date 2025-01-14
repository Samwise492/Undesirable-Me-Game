using UnityEngine;

public class InGameMenuToPuzzleSequenceAdapter : MonoBehaviour
{
    private InGameMenu menu => FindObjectOfType<InGameMenu>();

    private PuzzleSequence puzzleSequence => FindObjectOfType<PuzzleSequence>();

    private void OnEnable()
    {
        if (puzzleSequence)
        {
            puzzleSequence.OnSequenceStarted += Disable;
            puzzleSequence.OnSequenceEnded += Enable;
        }
    }
    private void OnDisable()
    {
        if (puzzleSequence)
        {
            puzzleSequence.OnSequenceStarted -= Disable;
            puzzleSequence.OnSequenceEnded -= Enable;
        }
    }

    private void Enable()
    {
        menu.enabled = true;
    }
    private void Disable()
    {
        menu.enabled = false;
    }
}