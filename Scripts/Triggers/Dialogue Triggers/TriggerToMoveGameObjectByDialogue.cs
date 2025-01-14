using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class TriggerToMoveGameObjectByDialogue : DialogueTrigger
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

    [Space]
    [SerializeField]
    private Transform objectToMove;
    [SerializeField]
    private float whereTo;
    [SerializeField]
    private float duration;
    [Tooltip("1 for axis. If more than one axis is set, nothing will happen.")]
    [SerializeField]
    private Vector3 axisForMove;

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
        if (axisForMove == new Vector3(1,0,0))
        {
            objectToMove.DOMoveX(whereTo, duration);
        }
        else if (axisForMove == new Vector3(0, 1, 0))
        {
            objectToMove.DOMoveY(whereTo, duration);
        }
        else if (axisForMove == new Vector3(0, 0, 1))
        {
            objectToMove.DOMoveZ(whereTo, duration);
        }
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
