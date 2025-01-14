using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class TriggerToStartDialogueByPuzzleController : BaseTrigger
{
	[SerializeField]
	private BaseDialogue dialogue;

	[SerializeField]
	private PuzzleSequence puzzleController;

    [SerializeField]
    private bool isDelayedDialoguePlay;
    [ShowIf("isDelayedDialoguePlay")]
    [SerializeField]
    private float delay;

    private void Start()
    {
        puzzleController.OnSequenceEnded += CheckPlay;
    }
    private void OnDestroy()
    {
        puzzleController.OnSequenceEnded -= CheckPlay;
    }

    public override void TriggerAction()
    {
        dialogue.Play();

        EndTrigger();
    }

    private void CheckPlay()
    {
        if (isDelayedDialoguePlay)
        {
            StartCoroutine(DelayStart());
        }
        else
        {
            TriggerAction();
        }
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(delay);

        TriggerAction();

        yield break;
    }
}
