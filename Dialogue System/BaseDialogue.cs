using System;
using System.Collections;
using UnityEngine;

public abstract class BaseDialogue : MonoBehaviour
{
    public event Action<DialogueData> OnDialogueStarted;
    public event Action<DialogueData> OnDialogueFinished;
    
    public event Action OnEnd;

    public bool isPlayOnStart;

    [HideInInspector]
    public float delayTime = 1.6f;

    [SerializeField]
    protected PlayerConfiguration playerConfiguration;

    private DialogueData currentDialogueData;

    private string[] currentDialogue = new string[] { };

    private int dialogueLineIndex = 0;

    private bool isAbleToTalk;
    private bool isDialogueGoing;
    private bool isDialogueFinished;

    protected virtual void Start()
    {
        StartCoroutine(CheckBehaviour());

        if (isPlayOnStart)
        {
            Play();
        }
    }
    protected virtual void OnDestroy()
    {
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

    public abstract void Play();

    public void PlayDialogue(DialogueData newDialogueData)
    {
        SetDialogueData(newDialogueData);

        OnDialogueStarted?.Invoke(currentDialogueData);

        if (currentDialogueData.PointsData != null)
        {
            playerConfiguration.AddPoints(currentDialogueData.PointsData);
        }
        if (currentDialogueData.KeyData != null)
        {
            playerConfiguration.ChangeKey(currentDialogueData.KeyData);
        }

        isAbleToTalk = true;
        isDialogueGoing = true;

        UIManager.Instance.SetWindow(UIManager.UIWindows.DialogueWindow);
        UIManager.Instance.SetDialogueWindow(currentDialogueData, dialogueLineIndex);
    }
    
    protected void NotifyEnd()
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
        }
    }
    
    private void EndDialogue()
    {
        NullifyText();

        isDialogueFinished = true;
        isDialogueGoing = false;
        isAbleToTalk = false;

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
            yield return new WaitUntil(() => Input.GetKeyDown(InputData.interactionKey));

            if (isAbleToTalk && UIManager.Instance.GetActiveWindow() != UIManager.UIWindows.ChoiceWindow)
            {
                if (isDialogueGoing)
                {
                    ChangeDialogueLine();

                    yield return new WaitForSeconds(delayTime);
                }
                else
                {
                    Play();

                    yield return new WaitForSeconds(delayTime);
                }

            }
        }
    }
}
