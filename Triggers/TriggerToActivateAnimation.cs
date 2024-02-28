using System.Collections;
using UnityEngine;

public class TriggerToActivateAnimation : MonoBehaviour
{
    [Header("Triggers")]
    [Tooltip("Turn off after it's finished")]
    [SerializeField]
    private bool isTurnOffAfterFinish;
    [SerializeField]
    private Dialogue dialogueTrigger;

    [Header("Manipulated object")]
    [SerializeField]
    private Animation objectWithAnimation;

    private void Start()
    {
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
                if (isTurnOffAfterFinish)
                    dialogueTrigger.enabled = false;

                objectWithAnimation.Play();

                yield break;
            }
        }
    }
}
