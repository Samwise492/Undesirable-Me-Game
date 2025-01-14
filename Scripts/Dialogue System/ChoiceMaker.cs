using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoiceMaker : MonoBehaviour
{
    /// <summary>
    /// We throw the dialogue that was choiced.
    /// </summary>
    public event Action<DialogueData> OnChoiceMade;

    [SerializeField]
    private DialogueChoiceData[] data;

    private bool isInited;

    private void Update()
    {
        if (isInited)
        {
            if (UIManager.Instance.GetActiveWindow() == UIManager.UIWindows.ChoiceWindow)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (Input.GetKeyDown((i + 1).ToString()))
                    {
                        MakeChoice(data[i].DialogueData);
                    }
                }
            }
        }
    }

    public void InitialiseChoices()
    {
        List<string> choiceLines = new();
        List<Action> actions = new();

        for (int i = 0; i < data.Length; i++)
        {
            int cachedIndex = i;

            choiceLines.Add(GetChoiceLineByLocalisation(data[cachedIndex].ChoiceLines));
            actions.Add(() => MakeChoice(data[cachedIndex].DialogueData));
        }

        isInited = true;

        ShowChoices(data.Length, choiceLines, actions);
    }

    private void ShowChoices(int choiceNumber, List<string> choiceLines, List<Action> actions)
    {
        UIManager.Instance.CreateChoiceLines(choiceNumber, choiceLines, actions);
        UIManager.Instance.SetWindow(UIManager.UIWindows.ChoiceWindow);
        
        CursorManager.Instance.SetCursor(true);
    }

    private void MakeChoice(DialogueData dialogueData)
    {
        UIManager.Instance.SetWindow(UIManager.UIWindows.DialogueWindow);
        CursorManager.Instance.SetCursor(false);

        OnChoiceMade?.Invoke(dialogueData);
    }

    private string GetChoiceLineByLocalisation(LocalisedChoiceLine[] choiceLines)
    {
        switch (PlayerSettingsFileManager.Instance.LoadData().language)
        {
            case Language.English:
                return choiceLines.Where(x => x.Language == Language.English).First().Line;
            case Language.Russian:
                return choiceLines.Where(x => x.Language == Language.Russian).First().Line;
        }

        return $"It's so weird, but I don't <color=red>remember</color> what to say now";
    }
}