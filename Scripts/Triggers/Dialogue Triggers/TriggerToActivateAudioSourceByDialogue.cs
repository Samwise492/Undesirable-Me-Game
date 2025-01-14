using UnityEngine;
using NaughtyAttributes;

public class TriggerToActivateAudioSourceByDialogue : DialogueTrigger
{
    [SerializeField]
    private bool isOnStart;
    [SerializeField]
    private bool isOnEndDialogueEnd = true;
    [SerializeField]
    private bool isOnSpecificDialogueEnd;
    [ShowIf("isOnSpecificDialogueEnd")]
    [SerializeField]
    private DialogueData requiredData;

    [Header("Manipulated object")]
    [SerializeField]
    private AudioSource audioSource;

    private bool isWasOnStart;

    protected override void Start()
    {
        if (!IsAvailable())
        {
            return;
        }

        if (isOnStart)
        {
            dialogueTrigger.OnDialogueStarted += CheckTriggerBehaviour;
        }
        else if (isOnEndDialogueEnd)
        {
            base.Start();
        }
        else if (isOnSpecificDialogueEnd)
        {
            dialogueTrigger.OnDialogueFinished += CheckDialogue;
        }
    }
    protected override void OnDestroy()
    {
        if (isOnStart)
        {
            dialogueTrigger.OnDialogueStarted -= CheckTriggerBehaviour;
        }
        else if (isOnEndDialogueEnd)
        {
            base.OnDestroy();
        }
        else if (isOnSpecificDialogueEnd)
        {
            dialogueTrigger.OnDialogueFinished -= CheckDialogue;
        }
    }

    public override void TriggerAction()
    {
        audioSource.Play();

        EndTrigger();
    }

    private void CheckDialogue(DialogueData data)
    {
        if (data == requiredData)
        {
            TriggerAction();
        }
    }

    private void CheckTriggerBehaviour(DialogueData data)
    {
        if (!isWasOnStart)
        {
            isWasOnStart = true;
            CheckTriggerBehaviour();
        }
    }
}