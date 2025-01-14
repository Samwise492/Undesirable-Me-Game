using UnityEngine;

public class TriggerToSwitchAnimationByCollider : ColliderTrigger
{
    [Space]
	[SerializeField]
	private Animator animator;

    [SerializeField]
    private string boolStateName;

    public override void TriggerAction()
    {
        animator.SetBool(boolStateName, true);

        EndTrigger();
    }
}
