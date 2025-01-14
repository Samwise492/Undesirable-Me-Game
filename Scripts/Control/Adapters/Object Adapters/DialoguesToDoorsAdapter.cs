using System.Collections;
using UnityEngine;

public class DialoguesToDoorsAdapter : MonoBehaviour
{
    private BaseDialogue[] dialogues => FindObjectsOfType<BaseDialogue>(true);
    private Door[] doors => FindObjectsOfType<Door>(true);

    private void OnEnable()
    {
        foreach (BaseDialogue dialogue in dialogues)
        {
            dialogue.OnDialogueFinished += AppendDelay;
        }
    }
    private void OnDisable()
    {
        foreach (BaseDialogue dialogue in dialogues)
        {
            dialogue.OnDialogueFinished -= AppendDelay;
        }
    }

    private void AppendDelay(DialogueData data)
    {
        StartCoroutine(ProcessDelay());
    }

    private IEnumerator ProcessDelay()
    {
        foreach (Door door in doors)
        {
            door.isAvailable = false;
        }

        yield return new WaitForSeconds(0.5f);

        foreach (Door door in doors)
        {
            door.isAvailable = true;
        }

        yield break;
    }
}
