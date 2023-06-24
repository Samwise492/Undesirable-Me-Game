using System.Collections;
using UnityEngine;

public class TriggeredActionMaker : MonoBehaviour
{
    [Tooltip("What action to do after trigger")] 
    [SerializeField] private ActionType actionType;

    [SerializeField] private Dialogue dialogueTrigger;
    [SerializeField] private ChoiceMaker choiceMakerTrigger;

    [Tooltip("Turn off after it's finished")]
    [SerializeField] private bool isTurnOffAfterFinish;
    [SerializeField] private int triggerChoiceLineIndex;

    private GameManager gameManager => FindObjectOfType<GameManager>();

    private Door door => GetComponent<Door>();
    private ChoiceMaker choiceMaker => GetComponent<ChoiceMaker>();
    private Dialogue dialogue => GetComponent<Dialogue>();

    private void Start()
    {
        if (dialogue)
            dialogue.enabled = false;
        if (choiceMaker)
            choiceMaker.enabled = false;
        if (door)
            door.enabled = false;

        AssignActionType();
    }
    private void OnDestroy()
    {
        if (dialogueTrigger)
            dialogueTrigger.OnDialogueFinished.RemoveAllListeners();
        if (choiceMaker)
            choiceMakerTrigger.OnChoiceMade.RemoveAllListeners();
    }

    private void AssignActionType()
    {
        switch (actionType)
        {
            case ActionType.ActivateDialogue:
                dialogueTrigger.OnDialogueFinished.AddListener(() => StartCoroutine(CheckForDialogueWindowClosure()));
                break;
            case ActionType.LoadScene:
                choiceMakerTrigger.OnChoiceMade.AddListener(LoadSceneAfterChoice);
                break;
        }
    }

    private void LoadSceneAfterChoice(int choiceIndex)
    {
        if (choiceIndex == triggerChoiceLineIndex)
            door.SwitchStage();
    }

    private IEnumerator CheckForDialogueWindowClosure()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (gameManager.GetActiveWindow() == null)
            {
                if (isTurnOffAfterFinish)
                    dialogueTrigger.enabled = false;

                if (dialogue)
                    dialogue.enabled = true;
                if (choiceMaker)
                    choiceMaker.enabled = true;

                yield break;
            }
        }
    }

    public enum ActionType
    {
        ActivateDialogue,
        LoadScene
    }
}