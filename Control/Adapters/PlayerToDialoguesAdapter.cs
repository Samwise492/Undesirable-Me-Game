using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerToDialoguesAdapter : MonoBehaviour
{
	private Player player => FindObjectOfType<Player>();

    private Dialogue[] dialoguesOnScene => FindObjectsOfType<Dialogue>(true);

    private void OnEnable()
    {
        foreach (Dialogue dialogue in dialoguesOnScene)
        {
            dialogue.OnDialogueStarted += ProhibitMovement;
            dialogue.OnDialogueFinished += AllowMovement;
        }
    }
    private void OnDisable()
    {
        foreach (Dialogue dialogue in dialoguesOnScene)
        {
            if (dialogue && player)
            {
                dialogue.OnDialogueStarted -= ProhibitMovement;
                dialogue.OnDialogueFinished -= AllowMovement;
            }
        }
    }

    private void AllowMovement(DialogueData dialogueData)
    {
        StartCoroutine(AppendDelay());
    }

    private IEnumerator AppendDelay()
    {
        yield return new WaitForSeconds(0.2f);

        if (UIManager.Instance.GetActiveWindow() != UIManager.UIWindows.ChoiceWindow && dialoguesOnScene.All(x => x.isBusyWithSequence != true))
        {
            player.AllowMovement();
        }

        yield break;
    }

    private void ProhibitMovement(DialogueData dialogueData)
    {
        player.ProhibitMovement();
    }
}
