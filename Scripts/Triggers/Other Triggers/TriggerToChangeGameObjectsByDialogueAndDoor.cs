using System.Linq;
using UnityEngine;

public class TriggerToChangeGameObjectsByDialogueAndDoor : BaseTrigger
{
    [Header("Triggers")]
    [SerializeField]
    private BaseDialogue dialogue;

    [SerializeField]
    private Door door;

    [Header("Manipulated Objects")]
    [SerializeField]
    private GameObject[] objectsToOn;

    [SerializeField]
    private GameObject[] objectsToOff;

    private bool isDialogueFinished;

    private void Start()
    {
        if (PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id))
        {
            isDialogueFinished = true;
            TriggerAction();
        }
        if (LogDataManager.Instance.GetPassedDialogues().Contains(dialogue.GetDialogue()))
        {
            isDialogueFinished = true;
        }

        dialogue.OnDialogueFinished += CheckDialogue;
        door.OnStageChange += TriggerAction;
    }
    private void OnDestroy()
    {
        dialogue.OnDialogueFinished -= CheckDialogue;
        door.OnStageChange -= TriggerAction;
    }

    public override void TriggerAction()
    {
        if (isDialogueFinished && GameState.Instance.CurrentState != CurrentGameState.IsLoadStageByProgress)
        {
            foreach (GameObject go in objectsToOn)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in objectsToOff)
            {
                go.SetActive(false);
            }

            EndTrigger();
        }
    }

    private void CheckDialogue(DialogueData data)
    {
        isDialogueFinished = true;
    }
}
