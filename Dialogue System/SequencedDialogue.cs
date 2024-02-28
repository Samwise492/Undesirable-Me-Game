using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencedDialogue : MonoBehaviour
{
    public event Action OnProcessingDelay;
    public event Action OnDelayProcessed;

    [SerializeField]
    private bool isAutoPlaySequence;

    [Space]
    [SerializeField]
    private List<SequencedDialogueData> sequence;

    [Header("Components")]
    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private ChoiceMaker choiceMaker;

    [Header("System")]
    [SerializeField]
    private float delayTime;

    private bool isInited;
    private int currentSequenceIndex;

    private List<DialogueData> history = new();

    private void Awake()
    {
        dialogue.OnDialogueStart += InitSequence;
        dialogue.OnDialogueFinished += ProcessSequence;
        choiceMaker.OnChoiceMade += PlayDialogueAfterChoice;

        if (isAutoPlaySequence)
        {
            dialogue.isAutoPlayDialogue = false;

            InitSequence();
        }
    }
    private void OnDestroy()
    {
        dialogue.OnDialogueStart -= InitSequence;
        dialogue.OnDialogueFinished -= ProcessSequence;
        choiceMaker.OnChoiceMade -= PlayDialogueAfterChoice;
    }

    private void InitSequence()
    {
        if (!isInited)
        {
            ProcessSequence();

            isInited = true;
        }
    }

    private void ProcessSequence(DialogueData dialogueData)
    {
        ProcessSequence();
    }
    private void ProcessSequence()
    {
        dialogue.isBusyWithSequence = true;

        if (currentSequenceIndex < sequence.Count)
        {
            CheckForDelay(sequence[currentSequenceIndex]);
        }
        else
        {
            dialogue.isBusyWithSequence = false;
        }

        currentSequenceIndex++;
    }

    private void CheckForDelay(SequencedDialogueData data)
    {
        if (!data.IsDelayBeforeIt)
        {
            CheckForAction(data);
        }
        else
        {
            StartCoroutine(ProcessDelay(data));
        }
    }

    private void CheckForAction(SequencedDialogueData data)
    {
        if (data.Type == SequenceDialogueDataType.Choice)
        {
            choiceMaker.InitialiseChoices();
        }
        else if (data.Type == SequenceDialogueDataType.Dialogue)
        {
            if (data.RequiredData != null)
            {
                if (history.Contains(data.RequiredData))
                {
                    SetDialogue(data.DialogueData);
                }
                else
                {
                    currentSequenceIndex++;

                    ProcessSequence();
                }
            }
            else
            {
                SetDialogue(data.DialogueData);
            }
        }
    }

    private void SetDialogue(DialogueData dialogueData)
    {
        dialogue.SetDialogueAndPlay(dialogueData);

        RememberHistory(dialogueData);
    }

    private void PlayDialogueAfterChoice(DialogueData dialogueData)
    {
        SetDialogue(dialogueData);
    }

    private void RememberHistory(DialogueData dataToRemember)
    {
        history.Add(dataToRemember);
    }

    private IEnumerator ProcessDelay(SequencedDialogueData data)
    {
        OnProcessingDelay?.Invoke();

        yield return new WaitForSeconds(delayTime);

        CheckForAction(data);

        OnDelayProcessed?.Invoke();

        yield break;
    }
}

[Serializable]
public class SequencedDialogueData
{
    public DialogueData DialogueData => dialogueData;
    public SequenceDialogueDataType Type => type;
    public bool IsDelayBeforeIt => isDelayBeforeIt;
    public DialogueData RequiredData => requiredData;

    [SerializeField]
    private SequenceDialogueDataType type;

    [SerializeField]
    private bool isDelayBeforeIt;

    [Header("For Dialogue")]
    [SerializeField]
    private DialogueData dialogueData;
    [Tooltip("If it's not null, we play 'dialogueData' only after this 'requiredData'")]
    [SerializeField]
    private DialogueData requiredData;
}

public enum SequenceDialogueDataType
{
    Dialogue,
    Choice
}