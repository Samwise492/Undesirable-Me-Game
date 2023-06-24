using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button RealStamp => realStamp;
    public Button FakeStamp => fakeStamp;

    [SerializeField] private RectTransform buttonWindow, dialogueWindow, choiceWindow;
    [SerializeField] private Button[] choiceLines;

    [Space]
    [SerializeField] private Text dialogueTextField;
    [SerializeField] private Image characterIconFrame, characterIcon;

    [Space]
    [SerializeField] private RectTransform stampWindow;
    [SerializeField] private Button realStamp, fakeStamp;

    private CursorManager cursor;

    private void Awake()
    {
        cursor = FindObjectOfType<CursorManager>(true);
        if (cursor != null) HandleCursor();
    }

    public UIWindows? GetActiveWindow()
    {
        if (buttonWindow.gameObject.activeInHierarchy)
            return UIWindows.ButtonWindow;
        else if (dialogueWindow.gameObject.activeInHierarchy)
            return UIWindows.DialogueWindow;
        else if (choiceWindow.gameObject.activeInHierarchy)
            return UIWindows.ChoiceWindow;

        return null;
    }
    public Button[] GetChoiceLines()
    {
        return choiceLines;
    }

    public void SetWindow(UIWindows? windowToTurnOn)
    {
        switch (windowToTurnOn)
        {
            case UIWindows.DialogueWindow:
                TurnOffAllWindows();
                dialogueWindow.gameObject.SetActive(true);
                break;
            case UIWindows.ButtonWindow:
                TurnOffAllWindows();
                buttonWindow.gameObject.SetActive(true);
                break;
            case UIWindows.ChoiceWindow:
                TurnOffAllWindows();
                choiceWindow.gameObject.SetActive(true);
                break;
            case UIWindows.StampWindow:
                TurnOffAllWindows();
                stampWindow.gameObject.SetActive(true);
                break;
            case null:
                TurnOffAllWindows();
                break;
        }
    }
    public void SetDialogueWindow(DialogueData data, int textLineIndex)
    {
        dialogueTextField.text = data.dialogue[textLineIndex];

        if (data.emotions[textLineIndex] != null)
        {
            characterIcon.sprite = data.emotions[textLineIndex];
            characterIconFrame.gameObject.SetActive(true);
        }
        else
            characterIconFrame.gameObject.SetActive(false);
    }
    public void SetCursorState(bool state)
    {
        if (cursor != null)
            cursor.gameObject.SetActive(state);
    }

    public void ClearDialogueTextField()
    {
        dialogueTextField.text = "";
    }

    private void TurnOffAllWindows()
    {
        buttonWindow.gameObject.SetActive(false);
        dialogueWindow.gameObject.SetActive(false);
        choiceWindow.gameObject.SetActive(false);
        stampWindow.gameObject.SetActive(false);
    }

    private void HandleCursor()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            cursor.gameObject.SetActive(false);
            cursor.isMute = true;
        }
        else
        {
            cursor.gameObject.SetActive(true);
            cursor.isMute = false;
        }
    }

    public enum UIWindows
    {
        DialogueWindow,
        ButtonWindow,
        ChoiceWindow,
        StampWindow
    }
}