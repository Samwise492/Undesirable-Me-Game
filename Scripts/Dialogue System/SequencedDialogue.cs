using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencedDialogue : BaseDialogue
{
    [SerializeField]
    private List<SequencedDialogueData> sequence;

    [Header("Components")]
    [SerializeField]
    private ChoiceMaker choiceMaker;

    private bool isInited;
    private int currentSequenceIndex;

    private readonly List<DialogueData> history = new();

    protected override void Start()
    {
        if (LogDataManager.Instance.GetPassedDialogues().Contains(sequence[0].DialogueData))
        {
            enabled = false;
            return;
        }

        base.Start();

        OnDialogueFinished += ProcessSequence;

        if (choiceMaker)
        {
            choiceMaker.OnChoiceMade += PlayDialogueAfterChoice;
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        OnDialogueFinished -= ProcessSequence;

        if (choiceMaker)
        {
            choiceMaker.OnChoiceMade -= PlayDialogueAfterChoice;
        }
    }

    public override DialogueData GetDialogue()
    {
        return sequence[0].DialogueData;
    }
    public override void InjectDialogueData(DialogueData dialogueData)
    {
        return;
    }
    protected override void AbstractPlay()
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
        if (currentSequenceIndex < sequence.Count)
        {
            CheckForDelay(sequence[currentSequenceIndex]);
        }
        else
        {
            NotifyDialogueEnd();
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
                    if (data.IsKeyRequired)
                    {
                        if (PlayerProgressProvider.Instance.GetKeyValue(data.RequiredKey))
                        {
                            SetNewDialogue(data.DialogueData);
                        }
                    }
                    else
                    {
                        SetNewDialogue(data.DialogueData);
                    }
                }
                else
                {
                    currentSequenceIndex++;

                    ProcessSequence();
                }
            }
            else
            {
                if (data.IsKeyRequired)
                {
                    if (PlayerProgressProvider.Instance.GetKeyValue(data.RequiredKey))
                    {
                        SetNewDialogue(data.DialogueData);
                    }
                    else
                    {
                        NotifyDialogueEnd();
                    }
                }
                else
                {
                    SetNewDialogue(data.DialogueData);
                }
            }
        }
    }

    private void SetNewDialogue(DialogueData dialogueData)
    {
        PlayDialogue(dialogueData);

        RememberHistory(dialogueData);
    }

    private void PlayDialogueAfterChoice(DialogueData dialogueData)
    {
        SetNewDialogue(dialogueData);
    }

    private void RememberHistory(DialogueData dataToRemember)
    {
        history.Add(dataToRemember);
    }

    private IEnumerator ProcessDelay(SequencedDialogueData data)
    {
        yield return new WaitForSeconds(data.DelayTime);

        CheckForAction(data);

        yield break;
    }
}

[Serializable]
public class SequencedDialogueData
{
    public DialogueData DialogueData => dialogueData;
    public SequenceDialogueDataType Type => type;
    public DialogueData RequiredData => requiredData;
    public bool IsDelayBeforeIt => isDelayBeforeIt;
    public float DelayTime => delayTime;
    public bool IsKeyRequired => isKeyRequired;
    public int RequiredKey => requiredKey;

    [SerializeField]
    private SequenceDialogueDataType type;

    [Header("Delay")]
    [SerializeField]
    private bool isDelayBeforeIt;
    [SerializeField]
    [Tooltip("Before this dialogue starts")]
    private float delayTime;

    [Header("Dialogue")]
    [SerializeField]
    private DialogueData dialogueData;
    [Tooltip("If it's not null, we play 'dialogueData' only after this 'requiredData'")]
    [SerializeField]
    private DialogueData requiredData;

    [SerializeField]
    private bool isKeyRequired;
    [SerializeField]
    private int requiredKey;
}

public enum SequenceDialogueDataType
{
    Dialogue,
    Choice
}