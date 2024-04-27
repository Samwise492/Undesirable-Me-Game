using UnityEngine;

public class TriggerToSwitchAnimationByDialogue : DialogueTrigger
{
    [Header("Manipulated Object")]
    [SerializeField]
    private Animator manipulatedAnimator;

    [SerializeField]
    private string boolStateName;

    [Space]
    [SerializeField]
    private Rigidbody2D rbToOff;

    protected override void Start()
    {
        base.Start();

        if (boolStateName != "")
        {
            manipulatedAnimator.SetBool(boolStateName, true);
        }

        if (rbToOff)
        {
            rbToOff.simulated = false;
        }
    }

    public override void TriggerAction()
    {
        manipulatedAnimator.SetBool(boolStateName, false);

        if (rbToOff != null)
        {
            rbToOff.simulated = true;
        }

        EndTrigger();
    }
}