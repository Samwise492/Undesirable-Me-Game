using UnityEngine;

public class PlayerToPuzzleSequenceAdapter : MonoBehaviour
{
	private Player player => FindObjectOfType<Player>();

	private PuzzleSequence sequence => FindObjectOfType<PuzzleSequence>();

    private void OnEnable()
    {
        sequence.OnSequenceStarted += player.ProhibitMovement;
        sequence.OnSequenceEnded += player.AllowMovement;
    }
    private void OnDisable()
    {
        if (sequence && player)
        {
            sequence.OnSequenceStarted -= player.ProhibitMovement;
            sequence.OnSequenceEnded -= player.AllowMovement;
        }
    }
}
