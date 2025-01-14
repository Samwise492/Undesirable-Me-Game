using UnityEngine;

public class TriggerToActivateAudioSourceByDialogueLine : BaseTrigger
{
    [SerializeField]
    private BaseDialogue dialogueTrigger;

    [SerializeField]
    private DialogueData requiredData;
    [SerializeField]
    private int dialogueLineToPlaySound;

    [Header("Manipulated object")]
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        dialogueTrigger.OnDialogueLineChanged += CheckForTrigger;
    }
    private void OnDestroy()
    {
        dialogueTrigger.OnDialogueLineChanged -= CheckForTrigger;
    }

    public override void TriggerAction()
    {
        audioSource.Play();

        EndTrigger();
    }

    private void CheckForTrigger(DialogueData data, int dialogueIndex)
    {
        if (dialogueIndex == dialogueLineToPlaySound && data == requiredData)
        {
            TriggerAction();
        }
    }
}