using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => instance;

    [Header("Windows")]
    [SerializeField]
    private CanvasGroup buttonWindow;
    [SerializeField]
    private CanvasGroup dialogueWindow;
    [SerializeField]
    private CanvasGroup choiceWindow;

    [Header("Dialogue UI")]
    [SerializeField] 
    private Text dialogueTextField;

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

    [Header("Cursor")]
    [SerializeField]
    private CursorManager cursor;

    private static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public UIWindows? GetActiveWindow()
    {
        if (buttonWindow.alpha == 1)
        { 
            return UIWindows.ButtonWindow;
        }
        else if (dialogueWindow.alpha == 1)
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
        for (int i = 0; i < quantity; i++)
        {
            Button choiceLine = Instantiate(choiceLinePrefab, choiceLineRoot);

            choiceLine.GetComponentInChildren<Text>().text = titles[i];

            UnityAction unityAction = new UnityAction(actions[i]);
            choiceLine.onClick.AddListener(unityAction);
        }
    }
    public void DestroyChoiceLines()
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
                break;
            case UIWindows.ButtonWindow:
                TurnOffAllWindows();
                buttonWindow.alpha = 1;
                break;
            case UIWindows.ChoiceWindow:
                TurnOffAllWindows();
                choiceWindow.alpha = 1;
                break;
            case null:
                TurnOffAllWindows();
                break;
        }
    }
    public void SetDialogueWindow(DialogueData data, int textLineIndex)
    {
        dialogueTextField.text = data.Dialogue[textLineIndex];

        Sprite emotionSprite = charactersData.GetEmotionSprite(data.Emotions[textLineIndex].character, data.Emotions[textLineIndex].mood);
        
        if (emotionSprite != null)
        {
            characterIcon.sprite = emotionSprite;
            characterIconFrame.gameObject.SetActive(true);
        }
        else
            characterIconFrame.gameObject.SetActive(false);
    }

    public void SetCursorState(bool state)
    {
       cursor.gameObject.SetActive(state);
    }

    public void ClearDialogueTextField()
    {
        dialogueTextField.text = "";
    }

    private void TurnOffAllWindows()
    {
        buttonWindow.alpha = 0;
        dialogueWindow.alpha = 0;
        choiceWindow.alpha = 0;
    }

    public enum UIWindows
    {
        DialogueWindow,
        ButtonWindow,
        ChoiceWindow
    }
}