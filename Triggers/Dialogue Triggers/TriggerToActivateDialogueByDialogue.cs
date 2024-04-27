using UnityEngine;

public class TriggerToActivateDialogueByDialogue : DialogueTrigger
{
    [Header("Manipulated object")]
    [SerializeField]
    private BaseDialogue dialogue;
    [SerializeField]
    private ChoiceMaker choiceMaker;

    protected override void Start()
    {
        base.Start();

        dialogue.enabled = false;
    }

    public override void TriggerAction()
    {
        dialogue.enabled = true;

        if (choiceMaker)
        {
            choiceMaker.enabled = true;
        }

        EndTrigger();
    }
}