using System.Collections;
using UnityEngine;

public class TriggerToActivateAudioSource : MonoBehaviour
{
    [Header("Triggers")]
    [Tooltip("Turn off after it's finished")]
    [SerializeField]
    private bool isTurnOffAfterFinish;
    [SerializeField]
    private Dialogue dialogueTrigger;

    [Header("Manipulated object")]
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        audioSource.enabled = false;

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

                audioSource.enabled = true;

                yield break;
            }
        }
    }
}
