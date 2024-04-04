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
    private Rigidbody2D rbToTurnOff;

    protected override void Start()
    {
        base.Start();

        if (boolStateName != "")
        {
            manipulatedAnimator.SetBool(boolStateName, true);
        }

        if (rbToTurnOff != null)
        {
            rbToTurnOff.simulated = false;
        }
    }

    public override void TriggerAction()
    {
        manipulatedAnimator.SetBool(boolStateName, false);

        if (rbToTurnOff != null)
        {
            rbToTurnOff.simulated = true;
        }
    }
}