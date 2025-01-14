using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public event Action OnPlay;

    [SerializeField]
    private CanvasGroup developersMenu;
    [SerializeField]
    private CanvasGroup settingsMenu;
    [SerializeField]
    private CanvasGroup chooseSaveMenu;
    [SerializeField]
    private CanvasGroup loadMenu;

    [Space]
    [SerializeField]
    private Button closeDevelopersButton;
    [SerializeField]
    private Button closeChooseSaveButton;
    [SerializeField]
    private Button closeLoadButton;

    [Space]
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button developersButton;
    [SerializeField]
    private Button changeLanguageButton;
    [SerializeField]
    private Button exitButton;

    [Space]
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button newGameButton;

    [Space]
    [SerializeField]
    private LoadMenu loadMenuScript;

    private void Start()
    {
        FadeScreenManager.Instance.FadeScreenOut();

        SubscribeButtons();

        HideAllWindows();

        CheckSaves();

        SetLanguageOnStart();
    }
    private void OnDestroy()
    {
        UnsubscribeButtons();
    }

    private void SubscribeButtons()
    {
        closeDevelopersButton.onClick.AddListener(HideDevelopers);
        closeChooseSaveButton.onClick.AddListener(HideChooseSave);
        closeLoadButton.onClick.AddListener(HideLoad);

        continueButton.onClick.AddListener(ContinueGame);
        playButton.onClick.AddListener(CheckStart);
        newGameButton.onClick.AddListener(StartNewGame);
        settingsButton.onClick.AddListener(ShowSettings);
        developersButton.onClick.AddListener(ShowDevelopers);
        changeLanguageButton.onClick.AddListener(ChangeLanguage);
        exitButton.onClick.AddListener(Exit);
    }
    private void UnsubscribeButtons()
    {
        closeDevelopersButton.onClick.RemoveListener(HideDevelopers);
        closeChooseSaveButton.onClick.RemoveListener(HideChooseSave);
        closeLoadButton.onClick.AddListener(HideLoad);

        continueButton.onClick.RemoveListener(ContinueGame);
        playButton.onClick.RemoveListener(CheckStart);
        newGameButton.onClick.RemoveListener(StartNewGame);
        settingsButton.onClick.RemoveListener(ShowSettings);
        developersButton.onClick.RemoveListener(ShowDevelopers);
        changeLanguageButton.onClick.RemoveListener(ChangeLanguage);
        exitButton.onClick.RemoveListener(Exit);
    }

    private void CheckStart()
    {
        if (PlayerSaveFileManager.Instance.LoadData().saves.Count == 0)
        {
            StartNewGame();
        }
        else
        {
            ShowWindow(chooseSaveMenu);
        }
    }
    private void ContinueGame()
    {
        loadMenuScript.UpdatePresentation();
        ShowWindow(loadMenu);
    }
    private void StartNewGame()
    {
        OnPlay?.Invoke();

        PlayerSaveFileManager.Instance.LoadData();
        LoadingManager.Instance.LoadScene(LoadingManager.Instance.SceneBootstrapData[0], false);
    }

    private void ShowDevelopers()
    {
        ShowWindow(developersMenu);
    }
    private void HideDevelopers()
    {
        HideWindow(developersMenu);
    }
    
    private void ShowSettings()
    {
        ShowWindow(settingsMenu);
    }

    private void HideChooseSave()
    {
        HideWindow(chooseSaveMenu);
    }

    private void HideLoad()
    {
        HideWindow(loadMenu);
    }

    private void SetLanguageOnStart()
    {
        Locale currentLocale = LocalizationSettings.AvailableLocales.Locales.Where(x => x.LocaleName == GetLocaleName()).First();
        int currentLocaleIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(currentLocale);

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLocaleIndex];
    }
    private void ChangeLanguage()
    {
        Locale currentLocale = LocalizationSettings.AvailableLocales.Locales.Where(x => x.LocaleName == GetLocaleName()).First();
        int currentLocaleIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(currentLocale);

        currentLocaleIndex++;

        if (currentLocaleIndex == LocalizationSettings.AvailableLocales.Locales.Count)
        {
            currentLocaleIndex = 0;
        }

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLocaleIndex];

        SaveLanguage();
    }

    private string GetLocaleName()
    {
        switch (PlayerSettingsFileManager.Instance.LoadData().language)
        {
            case Language.Russian:
                return "Russian (ru)";
            default:
                return "English (en)";
        }
    }

    private Language GetLanguage()
    {
        switch (LocalizationSettings.SelectedLocale.LocaleName)
        {
            case "Russian (ru)":
                return Language.Russian;
            default:
                return Language.English;
        }
    }
    private void SaveLanguage()
    {
        PlayerSettings playerSettings = PlayerSettingsFileManager.Instance.LoadData();
        playerSettings.language = GetLanguage();
        PlayerSettingsFileManager.Instance.SaveData(playerSettings);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void CheckSaves()
    {
        if (PlayerSaveFileManager.Instance.IsDataExist())
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    private void ShowWindow(CanvasGroup window)
    {
        HideAllWindows();

        window.alpha = 1;
        window.blocksRaycasts = true;
        window.interactable = true;
    }
    private void HideWindow(CanvasGroup window)
    {
        window.alpha = 0;
        window.blocksRaycasts = false;
        window.interactable = false;
    }
    private void HideAllWindows()
    {
        HideWindow(settingsMenu);
        HideWindow(developersMenu);
        HideWindow(chooseSaveMenu);
        HideWindow(loadMenu);
    }
}