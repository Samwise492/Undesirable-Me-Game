using System.Collections;
using UnityEngine;

public class PlayerToDialoguesAdapter : MonoBehaviour
{
    private Player player => FindObjectOfType<Player>();

    private BaseDialogue[] dialoguesOnScene => FindObjectsOfType<BaseDialogue>(true);

    private void OnEnable()
    {
        Invoke(nameof(Init), 0.2f);
    }
    private void OnDisable()
    {
        if (player)
        {
            foreach (BaseDialogue dialogue in dialoguesOnScene)
            {
                if (dialogue)
                {
                    dialogue.OnDialogueStarted -= ProhibitMovement;
                    dialogue.OnDialogueFinished -= AllowMovement;
                }
            }
        }
    }

    private void Init()
    {
        if (player)
        {
            foreach (BaseDialogue dialogue in dialoguesOnScene)
            {
                dialogue.OnDialogueFinished += AllowMovement;
                dialogue.OnDialogueStarted += ProhibitMovement;
            }

            if (UIManager.Instance.GetActiveWindow() == UIManager.UIWindows.DialogueWindow)
            {
                player.ProhibitMovement();
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
        yield return new WaitForSeconds(0.2f);

        if (UIManager.Instance.GetActiveWindow() != UIManager.UIWindows.ChoiceWindow)
        {
            player.AllowMovement();
        }

        yield break;
    }

    private IEnumerator ProcessProhibition()
    {
        yield return new WaitForSeconds(0.3f);

        player.ProhibitMovement();

        yield break;
    }
}