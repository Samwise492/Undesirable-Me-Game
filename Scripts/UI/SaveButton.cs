using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SaveButton : MonoBehaviour
{
    public event Action OnSaved;

    [HideInInspector]
    public int saveIndex;

    [SerializeField]
    private GameObject onSavedIndicator;

    [SerializeField]
    private TMP_Text dayText;
    [SerializeField]
    private TMP_Text locationText;

    private Button button => GetComponent<Button>();

    private void Start()
    {
        button.onClick.AddListener(Save);

        ChangePresentation();
        onSavedIndicator.SetActive(false);
    }
    private void OnDestroy()
    {
        button.onClick.RemoveListener(Save);
    }

    private void Save()
    {
        PlayerSaveLoadProvider.Instance.SaveTempByIndex(saveIndex, PlayerSaveLoadProvider.Instance.GatherSaveInfo());

        StartCoroutine(ShowSaved());

        ChangePresentation();

        OnSaved?.Invoke();
    }

    private void ChangePresentation()
    {
        if (PlayerSaveFileManager.Instance.LoadData().saves.Count <= saveIndex)
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

            PlayerSaveStorage data = PlayerSaveFileManager.Instance.LoadData();
            data.lastSaveIndex = saveIndex;

            PlayerSaveFileManager.Instance.SaveData(data);
        }
    }

    private void SetButtonText()
    {
        var stageDayToLoadFromSave = PlayerSaveFileManager.Instance.LoadData().saves[saveIndex].lastPackedSceneData.stageDayToLoad;
        var sceneToLoadFromSave = PlayerSaveFileManager.Instance.LoadData().saves[saveIndex].lastPackedSceneData.sceneToLoad;

        PackedSceneData sceneData = LoadingManager.Instance.SceneBootstrapData.Where
            (x => x.stageDayToLoad == stageDayToLoadFromSave && x.sceneToLoad == sceneToLoadFromSave).First();

        dayText.text = sceneData.localisedDayText.GetLocalizedString();
        locationText.text = sceneData.localisedLocationText.GetLocalizedString();
    }

    private IEnumerator ShowSaved()
    {
        onSavedIndicator.SetActive(true);

        yield return new WaitForSeconds(1f);

        onSavedIndicator.SetActive(false);

        yield break;
    }
}
