using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

public abstract class BaseDialogue : MonoBehaviour
{
    public event Action<DialogueData> OnDialogueStarted;
    public event Action<DialogueData> OnDialogueFinished;
    public event Action<DialogueData, int> OnDialogueLineChanged;

    public event Action OnEnd;

#if UNITY_EDITOR
    [ReadOnly]
#endif
    public bool isStopCheck;

    // ваще это не оч, надо делать бутстрапер (лучше со стейтами внутри) и в нем дергать триггеры когда надо. Не юзай старты и эвэйки лучше
    public bool isPlayOnStart;
    public bool isDelayBeforeFirstDialogue;
    [ShowIf("isDelayBeforeFirstDialogue")]
    [SerializeField]
    private float firstDialogueDelay;

    private DialogueData currentDialogueData;

    private string[] currentDialogue = new string[] { };

    private int dialogueLineIndex = 0;

    private bool isAbleToTalk;
    private bool isDialogueGoing;
    private bool isDialogueFinished;

    private bool wasDelayedOnStart;

    private void OnEnable()
    {
        isStopCheck = false;
        StartCoroutine(CheckBehaviour());
    }
    protected virtual void Start()
    {
        if (isPlayOnStart)
        {
            Play();
        }
    }
    protected virtual void OnDestroy()
    {
        isStopCheck = true;
        StopCoroutine(CheckBehaviour());
    }
    protected virtual void OnDisable()
    {
        isStopCheck = true;
        StopCoroutine(CheckBehaviour());
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
    private void Update()
    {
        print(DebugData.DialogueDebugKey + gameObject.name + $" {isAbleToTalk}");
    }

    public void Play()
    {
        if (isDelayBeforeFirstDialogue && !wasDelayedOnStart)
        {
            StartCoroutine(DoFirstDialogueDelay());
        }
        else
        {
            AbstractPlay();
        }
    }

    protected void PlayDialogue(DialogueData newDialogueData)
    {
        SetDialogueData(newDialogueData);

        OnDialogueStarted?.Invoke(currentDialogueData);

        if (currentDialogueData.PointsData.Length > 0)
        {
            PlayerProgressProvider.Instance.SetPoints(currentDialogueData.PointsData); 
        }
        if (currentDialogueData.KeyData.Length > 0)
        {
            PlayerProgressProvider.Instance.SetKeyValue(currentDialogueData.KeyData);
        }

        isAbleToTalk = true;
        isDialogueGoing = true;

        UIManager.Instance.SetWindow(UIManager.UIWindows.DialogueWindow);
        UIManager.Instance.SetDialogueWindow(currentDialogueData, dialogueLineIndex);
    }
    
    public abstract DialogueData GetDialogue();
    public abstract void InjectDialogueData(DialogueData dialogueData);
    protected abstract void AbstractPlay();

    protected void NotifyDialogueEnd()
    {
        OnEnd?.Invoke();
    }
    
    private void SetDialogueData(DialogueData dataToSet)
    {
        currentDialogueData = dataToSet;
        currentDialogueData.InitialiseDialogue();

        currentDialogue = dataToSet.Dialogue;
    }

    private void ChangeDialogueLine()
    {
        if (dialogueLineIndex + 1 == currentDialogue.Length)
        {
            UIManager.Instance.SetWindow(null);

            EndDialogue();
        }
        else
        {
            dialogueLineIndex++;

            UIManager.Instance.SetDialogueWindow(currentDialogueData, dialogueLineIndex);

            OnDialogueLineChanged?.Invoke(currentDialogueData, dialogueLineIndex);
        }
    }

    private void EndDialogue()
    {
        NullifyText();

        isDialogueFinished = true;
        isDialogueGoing = false;
        isAbleToTalk = false;

        LogDataManager.Instance.SaveToHistory(currentDialogueData);

        OnDialogueFinished?.Invoke(currentDialogueData);
    }
    private void NullifyText()
    {
        dialogueLineIndex = 0;

        UIManager.Instance.ClearDialogueTextField();
    }

    private IEnumerator CheckBehaviour()
    {
        while (true)
        {
            if (isStopCheck)
            {
                yield break;
            }

            yield return new WaitUntil(() => Input.GetKeyDown(InputData.interactionKey));

            if (isStopCheck)
            {
                yield break;
            }

            Debug.Log(DebugData.DialogueDebugKey + $"All good on {gameObject.name} (we need both): {isAbleToTalk}, " +
                      $"{UIManager.Instance.GetActiveWindow() != UIManager.UIWindows.ChoiceWindow}");
            if (isAbleToTalk && UIManager.Instance.GetActiveWindow() != UIManager.UIWindows.ChoiceWindow)
            {
                if (isDialogueGoing)
                {
                    ChangeDialogueLine();

                    yield return new WaitForSeconds(Constants.DelayBetweenDialogueLines);
                }
                else
                {
                    Play();

                    yield return new WaitForSeconds(Constants.DelayBetweenDialogueLines);
                }
            }
        }
    }
    private IEnumerator DoFirstDialogueDelay()
    {
        yield return new WaitForSeconds(firstDialogueDelay);

        wasDelayedOnStart = true;

        AbstractPlay();

        yield break;
    }
}