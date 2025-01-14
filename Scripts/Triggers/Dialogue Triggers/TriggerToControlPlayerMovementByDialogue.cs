using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerToControlPlayerMovementByDialogue : DialogueTrigger
{
    [SerializeField]
    private bool isStopMovement;
    
    [SerializeField]
    private bool isChangePlayerStateOnEnd;
    [ShowIf("isChangePlayerStateOnEnd")]
    [SerializeField]
    private float time;

    private Player player => FindObjectOfType<Player>();

    public override void TriggerAction()
    {
        CheckMovementBehaviourOnTriggerStart();
        
        if (isChangePlayerStateOnEnd)
        {
            StartCoroutine(WaitForChangeState());
        }
        
        EndTrigger();
    }

    private IEnumerator WaitForChangeState()
    {
        yield return new WaitForSeconds(time);

        CheckMovementBehaviourOnTriggerEnd();

        yield break;
    }

    private void CheckMovementBehaviourOnTriggerStart()
    {
        if (isStopMovement)
        {
            player.ProhibitMovement();
        }
        else
        {
            player.AllowMovement();
        }
    }
    private void CheckMovementBehaviourOnTriggerEnd()
    {
        if (isStopMovement)
        {
            player.AllowMovement();
        }
        else
        {
            player.ProhibitMovement();
        }
    }
}
