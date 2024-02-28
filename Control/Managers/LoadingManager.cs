using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public SceneData sceneData;

    public static LoadingManager Instance => instance;

    [Space]
    [SerializeField]
    private List<LoadingSceneData> loadingSceneData;

    private PackedSceneData receivedData;
    private Scene currentScene;

    private string bootTag = "boot";

    private static LoadingManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(PackedSceneData data)
    {
        receivedData = data;

        if (ParseMeta(receivedData.sceneToLoadMeta) == bootTag)
        {
            StartCoroutine(LoadSceneDelayed());
        }
        else
        {
            LoadSceneSimply();
        }
    }

    private string ParseMeta(string metaToParse)
    {
        List<char> rawMeta = metaToParse.ToCharArray().ToList().GetRange(3, 4);

        string parsedMetaTag = new(rawMeta.ToArray());

        return parsedMetaTag;
    }

    private void LoadSceneSimply()
    {
        SaveSceneData(receivedData.sceneToLoadMeta);

        SceneManager.LoadSceneAsync(receivedData.sceneToLoadName, LoadSceneMode.Single);
    }

    private IEnumerator LoadSceneDelayed()
    {
        currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync("Loading Screen", LoadSceneMode.Additive);

        yield return new WaitForSeconds(0.2f);

        LoadingScreenController.Instance.OnTitleLoaded += ChangeScenes;
        LoadingScreenController.Instance.ShowLoadingTitle(UnpackData(receivedData));

        yield break;
    }
    private void ChangeScenes()
    {
        SceneManager.UnloadSceneAsync(currentScene);
        LoadingScreenController.Instance.OnTitleLoaded -= ChangeScenes;

        //SceneManager.UnloadSceneAsync("Loading Screen");

        LoadSceneSimply();
    }

    private void SaveSceneData(string meta)
    {
        sceneData.lastTransferedSceneMeta = meta;
    }

    private string UnpackData(PackedSceneData data)
    {
        LoadingSceneData dataToCheck = loadingSceneData.Where(x => x.metaToParse == data.sceneToLoadMeta).First();

        if (dataToCheck != null)
        {
            return dataToCheck.textToShow;
        }

        Debug.LogError("Could not parse data!");

        return "";
    }
}

[Serializable]
public class LoadingSceneData
{
    public string textToShow;
    public string metaToParse; // meta of packedSceneData
}