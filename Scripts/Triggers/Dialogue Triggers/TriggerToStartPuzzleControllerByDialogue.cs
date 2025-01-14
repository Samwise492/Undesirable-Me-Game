using UnityEngine;

public class TriggerToStartPuzzleControllerByDialogue : DialogueTrigger
{
	[SerializeField]
	private PuzzleSequence controller; 

    public override void TriggerAction()
    {
        controller.StartPuzzleSequence();

        EndTrigger();
    }
}
