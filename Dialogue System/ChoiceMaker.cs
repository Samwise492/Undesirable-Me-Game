using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceMaker : MonoBehaviour
{
    /// <summary>
    /// We throw what dialogue choiced.
    /// </summary>
    public event Action<DialogueData> OnChoiceMade;

    public event Action<int, List<string>, List<Action>> OnInit;

    [SerializeField]
    private ChoiceContainer[] choiceContainers;

    private bool isInited;

    private void Update()
    {
        if (isInited)
        {
            if (UIManager.Instance.GetActiveWindow() == UIManager.UIWindows.ChoiceWindow)
            {
                for (int i = 0; i < choiceContainers.Length; i++)
                {
                    if (Input.GetKeyDown((i + 1).ToString()))
                    {
                        MakeChoice(choiceContainers[i].dialogueData);
                    }
                }
            }
        }
    }

    public void InitialiseChoices()
    {
        List<string> choiceLines = new();
        List<Action> actions = new();

        for (int i = 0; i < choiceContainers.Length; i++)
        {
            int cachedIndex = i;

            choiceLines.Add(choiceContainers[cachedIndex].choiceLine);
            actions.Add(() => MakeChoice(choiceContainers[cachedIndex].dialogueData));
        }

        isInited = true;

        OnInit?.Invoke(choiceContainers.Length, choiceLines, actions);
    }

    private void MakeChoice(DialogueData dialogueData)
    {
        UIManager.Instance.SetWindow(UIManager.UIWindows.DialogueWindow);

        OnChoiceMade?.Invoke(dialogueData);
    }
}

[Serializable]
public class ChoiceContainer
{
    public string choiceLine;
    public DialogueData dialogueData;
    public bool isRepeating;
}