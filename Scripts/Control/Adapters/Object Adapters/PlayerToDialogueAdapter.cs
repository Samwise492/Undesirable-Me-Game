using System.Collections;
using UnityEngine;

public class PlayerToDialogueAdapter : MonoBehaviour
{
    [SerializeField]
    private bool isAllowPlayerToMoveOnDialogueEnd;

    [SerializeField]
    private BaseDialogue dialogue;

    [SerializeField]
    private float delayTime = 0.2f;

    private Player player => FindObjectOfType<Player>();

    private void Start()
    {
        if (player)
        {
            // rewrite default behaviour
            dialogue.OnDialogueStarted -= ProhibitMovement;
            dialogue.OnDialogueFinished -= AllowMovement;

            dialogue.OnDialogueStarted += ProhibitMovement;

            if (isAllowPlayerToMoveOnDialogueEnd)
            {
                dialogue.OnDialogueFinished += AllowMovement;
            }
            else
            {
                dialogue.OnDialogueFinished += ProhibitMovement;
            }
        }
    }
    private void OnDisable()
    {
        if (player)
        {
            dialogue.OnDialogueStarted -= ProhibitMovement;

            if (isAllowPlayerToMoveOnDialogueEnd)
            {
                dialogue.OnDialogueFinished -= AllowMovement;
            }
            else
            {
                dialogue.OnDialogueFinished -= ProhibitMovement;
            }
        }
    }

    private void AllowMovement(DialogueData dialogueData)
    {
        StartCoroutine(ProcessAllowment());
    }

    private void ProhibitMovement(DialogueData dialogueData)
    {
        StartCoroutine(ProcessProhibition());
    }

    private IEnumerator ProcessAllowment()
    {
        yield return new WaitForSeconds(delayTime);

        if (UIManager.Instance.GetActiveWindow() != UIManager.UIWindows.ChoiceWindow)
        {
            player.AllowMovement();
        }

        yield break;
    }

    private IEnumerator ProcessProhibition()
    {
        yield return new WaitForSeconds(delayTime);

        player.ProhibitMovement();

        yield break;
    }
}
