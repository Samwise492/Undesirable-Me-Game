using System.Collections;
using UnityEngine;

public class TriggerToActivateDialogueByCollider : ColliderTrigger
{
    [SerializeField]
    private float delay;

    [SerializeField]
    private BaseDialogue dialogueToActivate;

    private void Start()
    {
        if (!PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id))
        {
            dialogueToActivate.enabled = false;
        }
    }

    public override void TriggerAction()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);

        dialogueToActivate.enabled = true;

        yield break;
    }
}
