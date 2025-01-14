using System.Collections;
using UnityEngine;

public class BaseTriggerToBaseDialogueAdapter : MonoBehaviour
{
    [SerializeField]
    private BaseTrigger trigger;

    [SerializeField]
    private BaseDialogue dialogue;

    [Space]
    [SerializeField]
    private float delay;

    private void OnEnable()
    {
        if (!PlayerSaveLoadProvider.Instance.GetCurrentSave().isGreeted 
            && LogDataManager.Instance.GetPassedDialogues().Contains(dialogue.GetDialogue()))
        {
            return;
        }

        dialogue.enabled = false;

        trigger.OnTriggerActionMade += StartSequence;
    }
    private void OnDisable()
    {
        trigger.OnTriggerActionMade -= StartSequence;
    }

    private void StartSequence()
    {
        StartCoroutine(DoDelay());
    }

    private IEnumerator DoDelay()
    {
        yield return new WaitForSeconds(delay);

        dialogue.enabled = true;

        dialogue.Play();

        yield break;
    }
}
