using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToChangeGameObjectsByDialogue : DialogueTrigger
{
    [SerializeField]
    private bool isOnStart;
    [SerializeField]
    private bool isOnEndDialogueEnd = true;
    [SerializeField]
    private bool isOnSpecificDialogueEnd;
    [ShowIf("isOnSpecificDialogueEnd")]
    [SerializeField]
    private List<DialogueData> requiredData;
    [SerializeField]
    private bool isIgnoreIsAvailable;

    [Space]
	[SerializeField]
	private GameObject[] objectsToOff;

	[SerializeField]
	private GameObject[] objectsToOn;

    private bool isWasOnStart;

    protected override void Start()
    {
        //base.Start();
        if (!isIgnoreIsAvailable)
        {
            if (!IsAvailable())
            {
                return;
            }
        }

        if (PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id) && GameState.Instance.CurrentState == CurrentGameState.IsLoadStageFromMenu)
        {
            TriggerAction();
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

    public override void TriggerAction()
    {
        foreach (GameObject obj in objectsToOff)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectsToOn)
        {
            obj.SetActive(true);
        }

        EndTrigger();
    }

    private void CheckDialogue(DialogueData data)
    {
        if (requiredData.Contains(data))
        {
            CheckTriggerBehaviour();
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