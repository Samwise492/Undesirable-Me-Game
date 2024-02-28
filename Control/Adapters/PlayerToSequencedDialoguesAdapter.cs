using UnityEngine;

public class PlayerToSequencedDialoguesAdapter : MonoBehaviour
{
	private Player player => FindObjectOfType<Player>();

	private SequencedDialogue[] sequencedDialogues => FindObjectsOfType<SequencedDialogue>(true);

    private void OnEnable()
    {
        foreach (SequencedDialogue sequencedDialogue in sequencedDialogues)
        {
            sequencedDialogue.OnProcessingDelay += player.ProhibitMovement;
        }
    }
    private void OnDisable()
    {
        foreach (SequencedDialogue sequencedDialogue in sequencedDialogues)
        {
            if (player != null)
            {
                sequencedDialogue.OnProcessingDelay -= player.ProhibitMovement;
            }
        }
    }
}
