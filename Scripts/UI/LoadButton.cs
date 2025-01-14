using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    public event Action OnLoaded;

    [HideInInspector]
    public int loadIndex;

    [SerializeField]
    private TMP_Text dayText;
    [SerializeField]
    private TMP_Text locationText;

    private Button button => GetComponent<Button>();

    private void Start()
    {
        button.onClick.AddListener(Load);

        ChangePresentation();
    }
    private void OnDestroy()
    {
        button.onClick.RemoveListener(Load);
    }

    private void Load()
    {
        PlayerSaveStorage.PlayerSave save = PlayerSaveFileManager.Instance.LoadData().saves[loadIndex];
        save.isGreeted = false;
        PlayerSaveLoadProvider.Instance.SaveInTemp(save);

        LoadingManager.Instance.LoadScene(save.lastPackedSceneData, true);

        OnLoaded?.Invoke();
    }

    public void ChangePresentation()
    {
        if (PlayerSaveFileManager.Instance.LoadData().saves.Count <= loadIndex)
        {
            switch (PlayerSettingsFileManager.Instance.LoadData().language)
            {
                case Language.English:
                    dayText.text = "Can't remember...";
                    break;
                case Language.Russian:
                    dayText.text = "Не могу вспомнить...";
                    break;
                default:
                    break;
            }
            locationText.text = "";
        }
        else
        {
            SetButtonText();
        }
    }

    private void SetButtonText()
    {
        var stageDayToLoadFromSave = PlayerSaveFileManager.Instance.LoadData().saves[loadIndex].lastPackedSceneData.stageDayToLoad;
        var sceneToLoadFromSave = PlayerSaveFileManager.Instance.LoadData().saves[loadIndex].lastPackedSceneData.sceneToLoad;

        PackedSceneData sceneData = LoadingManager.Instance.SceneBootstrapData.Where
            (x => x.stageDayToLoad == stageDayToLoadFromSave && x.sceneToLoad == sceneToLoadFromSave).First();

        dayText.text = sceneData.localisedDayText.GetLocalizedString();
        locationText.text = sceneData.localisedLocationText.GetLocalizedString();
    }
}
