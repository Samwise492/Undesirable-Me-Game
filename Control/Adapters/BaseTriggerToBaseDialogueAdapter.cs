using System.Collections;
using UnityEngine;

public class BaseTriggerToBaseDialogueAdapter : MonoBehaviour
{
    [SerializeField]
    private BaseTrigger trigger;

    [SerializeField]
    private BaseDialogue sequencedDialogue;

    [Space]
    [SerializeField]
    private float delay;

    private void OnEnable()
    {
        sequencedDialogue.enabled = false;

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

        sequencedDialogue.enabled = true;

        sequencedDialogue.Play();

        yield break;
    }
}
