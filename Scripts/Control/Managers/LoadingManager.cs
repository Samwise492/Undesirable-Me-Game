using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public event Action OnLoading;
    
    public static LoadingManager Instance { get; private set; }

    public PackedSceneData LastReceivedData => lastReceivedData;
    public List<PackedSceneData> SceneBootstrapData => sceneBootstrapData;

    [SerializeField]
    private List<PackedSceneData> sceneBootstrapData; // ну вот это костыль. Нам отсюда чисто локализованная дата нужна -
    // проверять по stageName и прочему как это сделано в обычном флоу загрузки сцены не надо. Уровень данных с уровнем
    // отображения путаешь. Лучше имей какую-то SO с ключом и значением в виде локализованной даты, ее дергай, а не 
    // такой список храни.

    private bool isLoadingFromMenu;

    private PackedSceneData lastReceivedData;
    private Scene currentScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void LoadScene(PackedSceneData data, bool isLoadingFromMenu)
    {
        OnLoading?.Invoke();

        lastReceivedData = data;
        this.isLoadingFromMenu = isLoadingFromMenu;

        if (lastReceivedData.isLoadingScreenRequired)
        {
            StartCoroutine(LoadSceneDelayed());
        }
        else
        {
            LoadSceneSimply();
        }
    }

    private void LoadSceneSimply()
    {
        SceneManager.LoadSceneAsync(lastReceivedData.sceneToLoad, LoadSceneMode.Single);
        
        if (isLoadingFromMenu)
        {
            GameState.Instance.TransitTo(CurrentGameState.IsLoadStageFromMenu);
        }
        else
        {
            GameState.Instance.TransitTo(CurrentGameState.IsLoadStageByProgress);
        }

        PlayerSaveStorage.PlayerSave save = PlayerSaveLoadProvider.Instance.GetCurrentSave();
        save.stage = lastReceivedData.stageToLoad;
        save.stageDay = lastReceivedData.stageDayToLoad;
        PlayerSaveLoadProvider.Instance.SaveInTemp(save);
    }

    private IEnumerator LoadSceneDelayed()
    {
        currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync("Loading Screen", LoadSceneMode.Additive);

        yield return new WaitForSeconds(0.2f);

        LoadingScreenPresenter.Instance.OnTitleLoaded += ChangeScenes;
        LoadingScreenPresenter.Instance.ShowLoadingTitle(GetTitle(lastReceivedData));

        yield break;
    }
    private void ChangeScenes()
    {
        SceneManager.UnloadSceneAsync(currentScene);
        LoadingScreenPresenter.Instance.OnTitleLoaded -= ChangeScenes;

        LoadSceneSimply();
    }

    private string GetTitle(PackedSceneData data)
    {
        PackedSceneData dataToCheck = sceneBootstrapData.Where(x => x.IsTheSame(data)).First();

        if (dataToCheck != null)
        {
            lastReceivedData.localisedDayText = dataToCheck.localisedDayText;
            lastReceivedData.localisedLocationText = dataToCheck.localisedLocationText;
            
            return dataToCheck.localisedDayText.GetLocalizedString();
        }

        Debug.LogError("Could not parse data!");

        return "";
    }
}

[Serializable]
public class PackedSceneData
{
    public string sceneToLoad;
    public int stageDayToLoad;
    public string stageToLoad;

    public bool isLoadingScreenRequired;
    public LocalizedString localisedDayText;
    public LocalizedString localisedLocationText;

    public bool IsTheSame(PackedSceneData dataToCheck)
    {
        if (sceneToLoad == dataToCheck.sceneToLoad)
        {
            if (stageDayToLoad == dataToCheck.stageDayToLoad)
            {
                if (stageToLoad == dataToCheck.stageToLoad)
                {
                    return true;
                }
            }
        }

        return false;
    }
}