using System.Collections;
using UnityEngine;

public class TriggerToActivateDoor : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField]
    private Dialogue dialogueTrigger;

    [Header("Manipulated object")]
    [SerializeField]
    private Door door;

    private void Start()
    {
        door.enabled = false;

        dialogueTrigger.OnDialogueFinished += StartChecking;
    }
    private void OnDestroy()
    {
        dialogueTrigger.OnDialogueFinished -= StartChecking;
    }

    private void StartChecking(DialogueData dialogueData)
    {
        StartCoroutine(CheckForDialogueWindowClosure());
    }

    private IEnumerator CheckForDialogueWindowClosure()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (UIManager.Instance.GetActiveWindow() == null)
            {
                door.enabled = true;

                yield break;
            }
        }
    }
}
