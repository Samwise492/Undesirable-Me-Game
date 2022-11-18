using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointsHandler : MonoBehaviour
{
    [SerializeField] PlayerPoints playerPoints;
    GameObject GUI;
    [SerializeField] float fadeSpeed;
    [SerializeField] Image background;
    Color defaultColor;
    bool _isLastDialogueStarted;
    Transform dialogueTextField;
    Transform fadingScreen;

    void Start()
    {
        GUI = GameObject.Find("GUI");
        dialogueTextField = GUI.transform.GetChild(0).GetChild(0).GetChild(0);
        fadingScreen = GUI.transform.GetChild(2);

        StartCoroutine(CheckForPoints());
    }

    IEnumerator CheckForPoints()
    {
        while (true)
        {
            // Death
            if (playerPoints.deathPoints >= 2 && !GUI.transform.GetChild(0).gameObject.activeSelf)
            {
                fadingScreen.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                StartCoroutine(FadeScreen());

                yield break;
            }
            else if (playerPoints.badPoints >= 2)
            {
                if (GameObject.Find("Doctor") != null) // if ending scene
                    _isLastDialogueStarted = GameObject.Find("Doctor").GetComponent<Talking>().isLastDialogueStarted;

                if (!dialogueTextField.gameObject.activeSelf && _isLastDialogueStarted)
                {
                    fadingScreen.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1);
                    StartCoroutine(FadeScreen());

                    yield break;
                }
            }
            else if (playerPoints.goodPoints >= 2)
            {
                if (GameObject.Find("Doctor") != null) // if ending scene
                    _isLastDialogueStarted = GameObject.Find("Doctor").GetComponent<Talking>().isLastDialogueStarted;

                if (!GUI.transform.GetChild(0).gameObject.activeSelf && _isLastDialogueStarted)
                {
                    fadingScreen.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1);
                    StartCoroutine(FadeScreen());

                    yield break;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeScreen()
    {
        Color varToDecrease = defaultColor;
        GameObject.FindObjectOfType<Player>().isAbleToMove = false;

        while (true)
        { 
            varToDecrease.a += fadeSpeed;
            background.color = varToDecrease;
            yield return new WaitForSeconds(0.15f);
        
            if (background.color.a >= 1)
            {
                SceneManager.LoadSceneAsync("End Scene", LoadSceneMode.Single);
                
                yield break;
            }
        }
    }  
}
