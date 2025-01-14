using System.Collections.Generic;
using UnityEngine;

public class TriggerToLoadSceneByDialogue : DialogueTrigger
{
    [Tooltip("If dialogueTrigger's finished DialogueData == this DialogueData, the scene will be loaded")]
    [SerializeField]
    private List<DialogueData> dataToCheck;

    [Header("Manipulated object")]
    [SerializeField]
    private Door door;

    [Header("Arguments")]
    [SerializeField]
    private PackedSceneData sceneToLoadData;

    protected override void Start()
    {
        if (!IsAvailable())
        {
            return;
        }

        if (isFadeBeforeSceneLoad)
        {
            dialogueTrigger.OnDialogueFinished += FadeScreen;
        }
        else
        {
            dialogueTrigger.OnDialogueFinished += LoadScene;
        }

        if (door)
        {
            door.enabled = false; // we must not have it available, otherwise the player can go through the door w/o trigger.
        }
    }
    protected override void OnDestroy()
    {
        dialogueTrigger.OnDialogueFinished -= FadeScreen;
        dialogueTrigger.OnDialogueFinished -= LoadScene;
    }

    private void FadeScreen(DialogueData eventArg)
    {
        if (dataToCheck.Contains(eventArg))
        {
            FadeScreenManager.Instance.FadeScreenIntoNewScene(sceneToLoadData);
        }
    }

    private void LoadScene(DialogueData eventArg)
    {
        if (dataToCheck.Contains(eventArg))
        {
            if (door)
            {
                door.SwitchStage();
            }
            else if (sceneToLoadData.sceneToLoad != "")
            {
                LoadingManager.Instance.LoadScene(sceneToLoadData, false);
            }
        }
    }

    public override void TriggerAction() { }
}