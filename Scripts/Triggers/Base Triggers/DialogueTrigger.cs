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
        if (!IsAvailable())
        {
            return;
        }

        dialogueTrigger.OnEnd += StartChecking;
    }
    protected virtual void OnDestroy()
    {
        if (dialogueTrigger)
        {
            dialogueTrigger.OnEnd -= StartChecking;
        }
    }

    protected bool IsAvailable() // как будто должно быть вообще дл€ всех видов тригеров. Ќу либо сделай опциональный флаг чтобы эта проверка не проходила
    {
        return !PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id);
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