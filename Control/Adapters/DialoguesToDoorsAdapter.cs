using System.Collections;
using UnityEngine;

public class DialoguesToDoorsAdapter : MonoBehaviour
{
    private Dialogue[] dialogues => FindObjectsOfType<Dialogue>(true);
    private Door[] doors => FindObjectsOfType<Door>(true);

    private void OnEnable()
    {
        foreach (Dialogue dialogue in dialogues)
        {
            dialogue.OnDialogueFinished += AppendDelay;
        }
    }
    private void OnDisable()
    {
        foreach (Dialogue dialogue in dialogues)
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
