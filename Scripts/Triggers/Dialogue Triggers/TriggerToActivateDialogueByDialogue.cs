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
        dialogue.enabled = false;

        base.Start();

        if (PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id))
        {
            TriggerAction();
        }
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