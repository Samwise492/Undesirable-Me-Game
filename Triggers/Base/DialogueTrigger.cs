using System.Collections;
using UnityEngine;

public abstract class DialogueTrigger : MonoBehaviour, ITrigger
{
    [Tooltip("Turn off dialogue trigger after it's finished")]
    [SerializeField]
    internal bool isTurnOffAfterFinish;

    [SerializeField]
	internal Dialogue dialogueTrigger;

    protected virtual void Start()
    {
        dialogueTrigger.OnDialogueFinished += StartChecking;
    }
    protected virtual void OnDestroy()
    {
        dialogueTrigger.OnDialogueFinished -= StartChecking;
    }

    public abstract void TriggerAction();

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
                if (isTurnOffAfterFinish)
                {
                    dialogueTrigger.enabled = false;
                }

                TriggerAction();

                yield break;
            }
        }
    }
}