using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup settingsMenu;
    [SerializeField]
    private Button closeButton;

    [Header("Data")]
    [SerializeField]
    private AudioMixerGroup SFXGroup;
    [SerializeField]
    private AudioMixerGroup musicGroup;

    [Header("Controls")]
    [SerializeField]
    private Button controlsButton;
    [SerializeField]
    private Button closeControlsButton;
    [SerializeField]
    private CanvasGroup controlsMenu;

    [Header("Display")]
    [SerializeField]
    private TMP_Dropdown displayModeDropdown;
    [SerializeField]
    private TMP_Dropdown displayResolutionDropdown;

    [Header("Music")]
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Toggle sfxMuteToggle;

    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Toggle musicMuteToggle;

    private PlayerSettings playerSettings;

    private void Start()
    {
        Init();

        Subscribe();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        closeButton.onClick.AddListener(HideSettings);

        sfxMuteToggle.onValueChanged.AddListener(MuteSFX);
        musicMuteToggle.onValueChanged.AddListener(MuteMusic);

        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);

        controlsButton.onClick.AddListener(ShowControlsMenu);
        closeControlsButton.onClick.AddListener(HideControlsMenu);

        displayModeDropdown.onValueChanged.AddListener(ChangeDisplayMode);
        displayResolutionDropdown.onValueChanged.AddListener(ChangeDisplayResolution);
    }
    private void Unsubscribe()
    {
        closeButton.onClick.RemoveListener(HideSettings);

        sfxMuteToggle.onValueChanged.RemoveListener(MuteSFX);
        musicMuteToggle.onValueChanged.RemoveListener(MuteMusic);

        sfxSlider.onValueChanged.RemoveListener(ChangeSFXVolume);
        musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);

        controlsButton.onClick.RemoveListener(ShowControlsMenu);
        closeControlsButton.onClick.RemoveListener(HideControlsMenu);

        displayModeDropdown.onValueChanged.RemoveListener(ChangeDisplayMode);
        displayResolutionDropdown.onValueChanged.RemoveListener(ChangeDisplayResolution);
    }

    private void HideSettings()
    {
        settingsMenu.alpha = 0;
        settingsMenu.blocksRaycasts = false;
        settingsMenu.interactable = false;

        PlayerSettingsFileManager.Instance.SaveData(playerSettings);
    }

    private void Init()
    {
        playerSettings = PlayerSettingsFileManager.Instance.LoadData();

        sfxMuteToggle.isOn = playerSettings.isSFXMuted;
        musicMuteToggle.isOn = playerSettings.isMusicMuted;

        musicSlider.value = playerSettings.musicVolume;
        sfxSlider.value = playerSettings.sfxVolume;

        SFXGroup.audioMixer.SetFloat("SFXVolume", playerSettings.isSFXMuted ? -80 : playerSettings.sfxVolume);
        musicGroup.audioMixer.SetFloat("MusicVolume", playerSettings.isMusicMuted ? -80 : playerSettings.musicVolume);

        Screen.SetResolution((int)playerSettings.displayResolutionX, (int)playerSettings.displayResolutionY, playerSettings.displayMode);

        HideControlsMenu();

        CreateDropdownOptions();
    }

    private void CreateDropdownOptions()
    {
        displayModeDropdown.ClearOptions();

        displayModeDropdown.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData { text = "Fullscreen" },
            new TMP_Dropdown.OptionData { text = "Windowed" }
        });

        displayResolutionDropdown.ClearOptions();

        displayResolutionDropdown.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData { text = "3840x2160" },
            new TMP_Dropdown.OptionData { text = "1920x1080" },
            new TMP_Dropdown.OptionData { text = "1366x768" },
            new TMP_Dropdown.OptionData { text = "1280x1024" },
            new TMP_Dropdown.OptionData { text = "1024x768" }
        });
    }

    private void ChangeDisplayResolution(int index)
    {
        int width = int.Parse(displayResolutionDropdown.options[index].text.Split('x')[0]);
        int height = int.Parse(displayResolutionDropdown.options[index].text.Split('x')[1]);

        Screen.SetResolution(width, height, playerSettings.displayMode);

        playerSettings.displayResolutionX = width;
        playerSettings.displayResolutionY = height;
    }

    private void ChangeDisplayMode(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }

        playerSettings.displayMode = Screen.fullScreenMode;
    }

    private void ShowControlsMenu()
    {
        controlsMenu.alpha = 1;
        controlsMenu.interactable = true;
        controlsMenu.blocksRaycasts = true;
    }
    private void HideControlsMenu()
    {
        controlsMenu.alpha = 0;
        controlsMenu.interactable = false;
        controlsMenu.blocksRaycasts = false;
    }

    private void MuteSFX(bool state)
    {
        sfxMuteToggle.isOn = state;

        playerSettings.isSFXMuted = state;
        SFXGroup.audioMixer.SetFloat("SFXVolume", playerSettings.isSFXMuted ? -80 : playerSettings.sfxVolume);

        PlayerSettingsFileManager.Instance.SaveData(playerSettings);
    }
    private void MuteMusic(bool state)
    {
        musicMuteToggle.isOn = state;

        playerSettings.isMusicMuted = state;
        musicGroup.audioMixer.SetFloat("MusicVolume", playerSettings.isMusicMuted ? -80 : playerSettings.musicVolume);

        PlayerSettingsFileManager.Instance.SaveData(playerSettings);
    }

    private void ChangeSFXVolume(float value)
    {
        playerSettings.sfxVolume = value;
        musicGroup.audioMixer.SetFloat("SFXVolume", value);

        if (value == -80)
        {
            MuteSFX(true);
        }
        else
        {
            MuteSFX(false);
        }
    }
    private void ChangeMusicVolume(float value)
    {
        playerSettings.musicVolume = value;
        musicGroup.audioMixer.SetFloat("MusicVolume", value);

        if (value == -80)
        {
            MuteMusic(true);
        }
        else
        {
            MuteMusic(false);
        }
    }
}