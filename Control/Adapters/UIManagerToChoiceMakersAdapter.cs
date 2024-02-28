using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerToChoiceMakersAdapter : MonoBehaviour
{
    private UIManager UIManager => FindObjectOfType<UIManager>();
    
    private ChoiceMaker[] choiceMakers => FindObjectsOfType<ChoiceMaker>(true);

    private void Start()
    {
        foreach (ChoiceMaker choiceMaker in choiceMakers)
        {
            choiceMaker.OnInit += ShowChoices;
        }
    }
    private void OnDisable()
    {
        foreach (ChoiceMaker choiceMaker in choiceMakers)
        {
            choiceMaker.OnInit -= ShowChoices;
        }
    }

    private void ShowChoices(int choiceNumber, List<string> choiceLines, List<Action> actions)
    {
        UIManager.CreateChoiceLines(choiceNumber, choiceLines, actions);

        UIManager.SetWindow(UIManager.UIWindows.ChoiceWindow);
    }
}
