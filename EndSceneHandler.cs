using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneHandler : MonoBehaviour
{
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] GameObject suicideScene, badScene, goodScene;

    void Start()
    {
        StartCoroutine(OpenScenes());
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
