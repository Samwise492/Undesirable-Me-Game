using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public event Action<bool> OnInGameMenuSet;

    [SerializeField]
    private CanvasGroup inGameMenu;
    [SerializeField]
    private CanvasGroup logMenu;
    [SerializeField]
    private CanvasGroup settingsMenu;
    [SerializeField]
    private CanvasGroup saveMenu;
    [SerializeField]
    private CanvasGroup loadMenu;

    [Space]
    [SerializeField]
    private Button logButton;
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button menuButton;

    [Space]
    [SerializeField]
    private LogMenu logMenuScript;
    [SerializeField]
    private LoadMenu loadMenuScript;
    [SerializeField]
    private SaveMenu saveMenuScript;

    private bool isInit;

    private void Start()
    {
        SubscribeAll();

        TurnOffAllCanvases();

        if (SceneManager.GetActiveScene().name != SceneNameData.MainMenu)
        {
            CheckLogs();
        }
    }
    private void OnDestroy()
    {
        UnsubcribeAll();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == SceneNameData.MainMenu || UIManager.Instance.GetActiveWindow() != null)
        {
            return;
        }

        if (Input.GetKeyDown(InputData.showInGameMenuKey))
        {
            if (!IsAnyWindowOpen())
            {
                SetInGameMenu(!(inGameMenu.alpha == 1));
            }
            else
            {
                TurnOffCurrentCanvas();
            }
        }
    }

    private void SubscribeAll()
    {
        logButton.onClick.AddListener(SetLogMenu);
        settingsButton.onClick.AddListener(SetSettingsMenu);
        menuButton.onClick.AddListener(LoadMainMenu);
        saveButton.onClick.AddListener(SetSaveMenu);
        loadButton.onClick.AddListener(SetLoadMenu);

        LogDataManager.Instance.OnHistoryChanged += CheckLogs;
        saveMenuScript.OnSaved += loadMenuScript.UpdatePresentation;
    }
    private void UnsubcribeAll()
    {
        logButton.onClick.RemoveListener(SetLogMenu);
        settingsButton.onClick.RemoveListener(SetSettingsMenu);
        menuButton.onClick.RemoveListener(LoadMainMenu);
        saveButton.onClick.RemoveListener(SetSaveMenu);
        loadButton.onClick.RemoveListener(SetLoadMenu);

        LogDataManager.Instance.OnHistoryChanged -= CheckLogs;
    }

    private bool IsAnyWindowOpen()
    {
        if (logMenu.alpha == 1 || settingsMenu.alpha == 1 || saveMenu.alpha == 1 || loadMenu.alpha == 1)
        {
            return true;
        }

        return false;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneNameData.MainMenu, LoadSceneMode.Single);
    }

    private void SetLoadMenu()
    {
        TurnOffChildCanvases(loadMenu);

        SetLoadMenu(loadMenu.alpha == 0);
    }
    private void SetLoadMenu(bool state)
    {
        if (state)
        {
            loadMenuScript.UpdatePresentation();
        }

        loadMenu.alpha = state ? 1 : 0;
        loadMenu.interactable = state;
        loadMenu.blocksRaycasts = state;
    }

    private void SetSaveMenu()
    {
        TurnOffChildCanvases(saveMenu);

        SetSaveMenu(saveMenu.alpha == 0);
    }
    private void SetSaveMenu(bool state)
    {
        saveMenu.alpha = state ? 1 : 0;
        saveMenu.interactable = state;
        saveMenu.blocksRaycasts = state;
    }

    private void SetInGameMenu(bool state)
    {
        inGameMenu.alpha = state ? 1 : 0;
        inGameMenu.interactable = state;
        inGameMenu.blocksRaycasts = state;

        if (!state)
        {
            TurnOffChildCanvases();
        }

        if (isInit)
        {
            CursorManager.Instance.SetCursor(state);
        }
        isInit = true;

        OnInGameMenuSet?.Invoke(state);
    }

    private void SetSettingsMenu()
    {
        TurnOffChildCanvases(settingsMenu);

        SetSettingsMenu(settingsMenu.alpha == 0);
    }
    private void SetSettingsMenu(bool state)
    {
        if (state)
        {
            settingsMenu.alpha = 1;
            settingsMenu.interactable = true;
            settingsMenu.blocksRaycasts = true;
        }
        else
        {
            settingsMenu.alpha = 0;
            settingsMenu.interactable = false;
            settingsMenu.blocksRaycasts = false;
        }
    }

    private void SetLogMenu()
    {
        TurnOffChildCanvases(logMenu);

        SetLogMenu(logMenu.alpha == 0);
    }
    private void SetLogMenu(bool state)
    {
        logMenuScript.ClearLogs();

        if (state)
        {
            logMenu.alpha = 1;
            logMenu.interactable = true;
            logMenu.blocksRaycasts = true;

            logMenuScript.CreateLogs();
        }
        else
        {
            logMenu.alpha = 0;
            logMenu.interactable = false;
            logMenu.blocksRaycasts = false;
        }
    }

    private void CheckLogs()
    {
        logButton.interactable = LogDataManager.Instance.GetLogs().Count > 0;
    }

    private void TurnOffCurrentCanvas()
    {
        if (saveMenu.alpha == 1)
        {
            SetSaveMenu(false);
        }
        else if (loadMenu.alpha == 1)
        {
            SetLoadMenu(false);
        }
        else if (logMenu.alpha == 1)
        {
            SetLogMenu(false);
        }
        else if (settingsMenu.alpha == 1)
        {
            SetSettingsMenu(false);
        }
    }
    private void TurnOffAllCanvases()
    {
        SetInGameMenu(false);

        TurnOffChildCanvases();
    }
    private void TurnOffChildCanvases()
    {
        SetLogMenu(false);
        SetSettingsMenu(false);
        SetSaveMenu(false);
        SetLoadMenu(false);
    }
    private void TurnOffChildCanvases(CanvasGroup canvas)
    {
        if (canvas == logMenu)
        {
            SetSettingsMenu(false);
            SetSaveMenu(false);
            SetLoadMenu(false);
        }
        else if (canvas == settingsMenu)
        {
            SetLogMenu(false);
            SetSaveMenu(false);
            SetLoadMenu(false);
        }
        else if (canvas == loadMenu)
        {
            SetLogMenu(false);
            SetSettingsMenu(false);
            SetSaveMenu(false);
        }
        else if (canvas == saveMenu)
        {
            SetLogMenu(false);
            SetSettingsMenu(false);
            SetLoadMenu(false);
        }
    }
}