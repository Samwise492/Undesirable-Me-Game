using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class TriggerToControlPlayerMovementByCollider : ColliderTrigger
{
    [SerializeField]
    private bool isStopMovement;

    [ShowIf("isStopMovement")]
    [SerializeField]
    private bool isAllowOnEnd;
    [ShowIf("isAllowOnEnd")] 
    [SerializeField]
    private float stopMovementTime;

    private Player player => FindObjectOfType<Player>();

    private bool isTriggered;

    private void Update()
    {
        if (isTriggered)
        {
            player.ProhibitMovement();
        }
    }

    public override void TriggerAction()
    {
        if (isStopMovement)
        {
            isTriggered = true;

            if (isAllowOnEnd)
            {
                StartCoroutine(WaitForAllowance());
            }

            EndTrigger();
        }
    }

    private IEnumerator WaitForAllowance()
    {
        yield return new WaitForSeconds(stopMovementTime);

        isTriggered = false;
        player.AllowMovement();

        yield break;
    }
}
