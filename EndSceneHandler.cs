using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneHandler : MonoBehaviour
{
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] GameObject suicideScene, badScene, goodScene;

    void Start()
    {
        StartCoroutine(OpenScenes());
    }
    void Update()
    {
        if (suicideScene.activeSelf || badScene.activeSelf || goodScene.activeSelf)
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync("Main menu", LoadSceneMode.Single);
                playerPoints.badPoints = 0;
                playerPoints.goodPoints = 0;
                playerPoints.deathPoints = 0;
            }
    }
    IEnumerator OpenScenes()
    {
        if (playerPoints.deathPoints >= 2)
        {
            yield return new WaitForSeconds(1);
            suicideScene.SetActive(true);
        }
        else if (playerPoints.badPoints >= 2)
        {
            yield return new WaitForSeconds(1);
            badScene.SetActive(true);
        }
        else if (playerPoints.goodPoints >= 2)
        {
            yield return new WaitForSeconds(1);
            goodScene.SetActive(true);
        }
    }
}
