using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private PackedSceneData firstSceneData;

    [Space]
    [SerializeField]
    private CanvasGroup controls;
    [SerializeField]
    private CanvasGroup developers;

    [Space]
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button showControlsButton;
    [SerializeField]
    private Button showDevelopersButton;

    private void Start()
    {
        HideAllWindows();

        SubscribeButtons();
    }
    private void OnDestroy()
    {
        UnsubscribeButtons();
    }

    public void CloseWindow()
    {
        CanvasGroup window = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<CanvasGroup>();

        if (window != null)
        {
            HideWindow(window);
        }
        else
        {
            Debug.LogError("Window you tried to close has no Canvas Group component on it.");
        }
    }

    public void Play()
    {
        LoadingManager.Instance.LoadScene(firstSceneData);
    }
    private void ShowControls()
    {
        ShowWindow(controls);
    }
    private void ShowDevelopers()
    {
        ShowWindow(developers);
    }
    private void Exit()
    {
        Application.Quit();
    }

    private void SubscribeButtons()
    {
        playButton.onClick.AddListener(Play);
        exitButton.onClick.AddListener(Exit);
        showControlsButton.onClick.AddListener(ShowControls);
        showDevelopersButton.onClick.AddListener(ShowDevelopers);
    }
    private void UnsubscribeButtons()
    {
        playButton.onClick.RemoveListener(Play);
        exitButton.onClick.RemoveListener(Exit);
        showControlsButton.onClick.RemoveListener(ShowControls);
        showDevelopersButton.onClick.RemoveListener(ShowDevelopers);
    }

    private void ShowWindow(CanvasGroup window)
    {
        HideAllWindows();

        window.alpha = 1;
        window.blocksRaycasts = true;
    }
    private void HideWindow(CanvasGroup window)
    {
        window.alpha = 0;
        window.blocksRaycasts = false;
    }

    private void HideAllWindows()
    {
        HideWindow(controls);
        HideWindow(developers);
    }
}