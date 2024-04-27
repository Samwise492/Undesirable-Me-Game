using System.Collections;
using UnityEngine;

public abstract class DialogueTrigger : BaseTrigger
{
    [Tooltip("Turn off dialogue trigger after it's finished")]
    [SerializeField]
    internal bool isTurnOffAfterFinish;

    [SerializeField]
    internal BaseDialogue dialogueTrigger;

    protected virtual void Start()
    {
        dialogueTrigger.OnEnd += StartChecking;
    }
    protected virtual void OnDestroy()
    {
        dialogueTrigger.OnEnd -= StartChecking;
    }

    private void StartChecking()
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

                CheckTriggerBehaviour();

                yield break;
            }
        }
    }
}