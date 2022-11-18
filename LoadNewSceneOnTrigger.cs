using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneOnTrigger : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] GameObject triggerObject;
    Talking talkingComponent;
    Canvas GUI;

    void Start()
    {
        talkingComponent = triggerObject.GetComponent<Talking>();
        GUI = GameObject.Find("GUI").GetComponent<Canvas>();
    }

    void Update()
    {
        if (talkingComponent.IsDialogueFinished && !GUI.transform.GetChild(0).gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {  
                if (sceneToLoad.StartsWith("Day"))
                    StartCoroutine(LoadNewDay(Int32.Parse(sceneToLoad.Split(' ')[1])));
                else
                    StartCoroutine(LoadNewScene());
            }
        }
    }

    IEnumerator LoadNewScene()
    {
        Scene previousActiveScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync("Loading Screen", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.2f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading Screen"));

        foreach(Transform child in GameObject.Find("Days").transform)
        {
            child.gameObject.SetActive(false);
        }

        SceneManager.UnloadSceneAsync(previousActiveScene);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        yield break;
    }
    IEnumerator LoadNewDay(int dayToLoad)
    {
        Scene previousActiveScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync("Loading Screen", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.2f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading Screen"));

        foreach(Transform child in GameObject.Find("Days").transform)
        {
            child.gameObject.SetActive(true);
        }

        GameObject dayToLoadObject = GameObject.Find("Day " + dayToLoad.ToString());
        dayToLoadObject.SetActive(true);

        foreach(Transform child in GameObject.Find("Days").transform)
        {
            if (child.name != dayToLoadObject.name)
            {
                child.gameObject.SetActive(false);
            }
        }

        SceneManager.UnloadSceneAsync(previousActiveScene);

        yield break;
    }
}
