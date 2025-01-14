using UnityEngine;

public class TriggerToActivateAnimationByDialogue : DialogueTrigger
{
    [Header("Manipulated object")]
    [SerializeField]
    private Animation objectWithAnimation;

    public override void TriggerAction()
    {
        objectWithAnimation.Play();

        EndTrigger();
    }
}
