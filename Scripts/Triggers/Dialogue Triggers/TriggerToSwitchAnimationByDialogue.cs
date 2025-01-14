using UnityEngine;

public class TriggerToSwitchAnimationByDialogue : DialogueTrigger
{
    [Header("Manipulated Object")]
    [SerializeField]
    private Animator manipulatedAnimator;

    [SerializeField]
    private string boolStateName;

    [SerializeField] 
    private bool startValueIsFalse;
    
    [Space]
    [SerializeField]
    private Rigidbody2D rbToOff;

    protected override void Start()
    {
        base.Start();

        if (!IsAvailable())
        {
            return;
        }

        if (!startValueIsFalse)
        {
            TurnOnAnimation();
        }
        else
        {
            TurnOffAnimation();
        }
    }

    public override void TriggerAction()
    {
        if (!startValueIsFalse)
        {
            TurnOffAnimation();
        }
        else
        {
            TurnOnAnimation();
        }

        EndTrigger();
    }

    private void TurnOffAnimation()
    {
        manipulatedAnimator.SetBool(boolStateName, false);

        if (rbToOff != null)
        {
            rbToOff.simulated = true;
        }
    }
    private void TurnOnAnimation()
    {
        if (boolStateName != "")
        {
            manipulatedAnimator.SetBool(boolStateName, true);
        }

        if (rbToOff)
        {
            rbToOff.simulated = false;
        }
    }
}