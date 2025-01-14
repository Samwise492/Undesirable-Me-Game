using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance;

    [Header("Windows")]
    [SerializeField]
    private CanvasGroup dialogueWindow;
    [SerializeField]
    private CanvasGroup choiceWindow;

    [Header("Dialogue UI")]
    [SerializeField]
    private TextMeshProUGUI dialogueTextField;

    [Space]
    [SerializeField]
    private Image characterIconFrame;
    [SerializeField]
    private Image characterIcon;

    [Space]
    [SerializeField]
    private Button choiceLinePrefab;
    [SerializeField]
    private Transform choiceLineRoot;

    [Header("Data")]
    [SerializeField]
    private CharactersData charactersData;

    private static UIManager instance;

    private void Awake()
    {
        instance = this;

        TurnOffAllWindows();
    }

    public UIWindows? GetActiveWindow()
    {
        if (dialogueWindow.alpha == 1)
        {
            return UIWindows.DialogueWindow;
        }
        else if (choiceWindow.alpha == 1)
        {
            return UIWindows.ChoiceWindow;
        }

        return null;
    }

    public void CreateChoiceLines(int quantity, List<string> titles, List<Action> actions)
    {
        DestroyChoiceLines();

        for (int i = 0; i < quantity; i++)
        {
            Button choiceLine = Instantiate(choiceLinePrefab, choiceLineRoot);

            choiceLine.GetComponentInChildren<TextMeshProUGUI>().text = titles[i];

            UnityAction unityAction = new UnityAction(actions[i]);
            choiceLine.onClick.AddListener(unityAction);
        }
    }
    private void DestroyChoiceLines()
    {
        foreach (Transform child in choiceLineRoot)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetWindow(UIWindows? windowToTurnOn)
    {
        switch (windowToTurnOn)
        {
            case UIWindows.DialogueWindow:
                TurnOffAllWindows();
                dialogueWindow.alpha = 1;
                dialogueWindow.interactable = true;
                dialogueWindow.blocksRaycasts = true;
                break;
            case UIWindows.ChoiceWindow:
                TurnOffAllWindows();
                choiceWindow.alpha = 1;
                choiceWindow.interactable = true;
                choiceWindow.blocksRaycasts = true;
                break;
            case null:
                TurnOffAllWindows();
                break;
        }
    }
    public void SetDialogueWindow(DialogueData data, int textLineIndex)
    {
        dialogueTextField.text = "";
        string formattedLine = DialogueParser.FormatDialogue(data.Dialogue[textLineIndex]);
        dialogueTextField.DOText(formattedLine, 1.5f, true, ScrambleMode.None);

        Sprite emotionSprite = charactersData.GetEmotionSprite(data.Emotions[textLineIndex].character, data.Emotions[textLineIndex].mood);

        if (emotionSprite != null)
        {
            characterIcon.sprite = emotionSprite;
            characterIconFrame.gameObject.SetActive(true);
        }
        else
        {
            characterIconFrame.gameObject.SetActive(false);
        }
    }

    public void ClearDialogueTextField()
    {
        dialogueTextField.text = "";
    }

    private void TurnOffAllWindows()
    {
        dialogueWindow.alpha = 0;
        choiceWindow.alpha = 0;

        dialogueWindow.interactable = false;
        choiceWindow.interactable = false;

        dialogueWindow.blocksRaycasts = false;
        choiceWindow.blocksRaycasts = false;
    }

    public enum UIWindows
    {
        DialogueWindow,
        ChoiceWindow
    }
}