using System;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public event Action OnDialogueStart;
    public event Action<DialogueData> OnDialogueStarted;
    public event Action<DialogueData> OnDialogueFinished;

    public bool isAutoPlayDialogue;

    [HideInInspector]
    public bool isBusyWithSequence;

    [SerializeField]
    private DialogueData dialogueData;
    [SerializeField]
    private PlayerConfiguration playerConfiguration;

    private string[] currentDialogue = new string[] { };

    private int dialogueLineIndex = 0;

    private bool isAbleToTalk;
    private bool isDialogueGoing;
    private bool isDialogueFinished;

    private void Start()
    {
        if (dialogueData != null)
        {
            SetDialogue(dialogueData);
        }

        if (isAutoPlayDialogue)
        {
            PlayDialogue();
        }
    }

    private void Update()
    {
        if (isAbleToTalk)
        {
            if (Input.GetKeyDown(InputData.interactionKey) && UIManager.Instance.GetActiveWindow() != UIManager.UIWindows.ChoiceWindow)
            {
                if (isDialogueGoing)
                {
                    ChangeDialogueLine();
                }
                else
                {
                    OnDialogueStart?.Invoke();

                    PlayDialogue();
                }
            }
        }
    }

    private void OnTriggerEnter2D()
    {
        if (!isDialogueFinished)
        {
            isAbleToTalk = true;
        }
    }
    private void OnTriggerExit2D()
    {
        if (!isDialogueFinished)
        {
            isAbleToTalk = false;
        }
    }

    public void PlayDialogue()
    {
        OnDialogueStarted?.Invoke(dialogueData);

        if (dialogueData.PointsData != null)
        {
            playerConfiguration.AddPoints(dialogueData.PointsData);
        }
        if (dialogueData.KeyData != null)
        {
            playerConfiguration.ChangeKey(dialogueData.KeyData);
        }

        isAbleToTalk = true;
        isDialogueGoing = true;

        UIManager.Instance.SetWindow(UIManager.UIWindows.DialogueWindow);
        UIManager.Instance.SetDialogueWindow(dialogueData, dialogueLineIndex);
    }

    public void SetDialogueAndPlay(DialogueData newDialogueData)
    {
        SetDialogue(newDialogueData);
        PlayDialogue();
    }

    public void SetDialogue(DialogueData dataToSet)
    {
        dialogueData = dataToSet;
        dialogueData.InitialiseDialogue();

        currentDialogue = dataToSet.Dialogue;
    }

    private void ChangeDialogueLine()
    {
        if (dialogueLineIndex + 1 == currentDialogue.Length) // dialogueLineIndex + 1
        {
            UIManager.Instance.SetWindow(null);

            EndDialogue();
        }
        else
        {
            dialogueLineIndex++;

            UIManager.Instance.SetDialogueWindow(dialogueData, dialogueLineIndex);
        }
    }

    private void EndDialogue()
    {
        NullifyText();

        isDialogueFinished = true;
        isDialogueGoing = false;
        isAbleToTalk = false;

        OnDialogueFinished?.Invoke(dialogueData);
    }

    private void NullifyText()
    {
        dialogueLineIndex = 0;

        UIManager.Instance.ClearDialogueTextField();
    }
}