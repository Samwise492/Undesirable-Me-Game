using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    Text textToFade;
    Color defaultColor;
    bool isIncreasing = true;
    bool isDecreasing = false;

    void Start()
    {
        textToFade = this.GetComponent<Text>();
        defaultColor = textToFade.color;
        StartCoroutine(_FadeText());
    }

    IEnumerator _FadeText()
    {
        Color varToDecrease = defaultColor;

        while (true)
        { 
            if (textToFade.color.a < 1 && isIncreasing)
            {
                varToDecrease.a += fadeSpeed;
                textToFade.color = varToDecrease;
                yield return new WaitForSeconds(0.15f);
            }
            else 
            {
                isIncreasing = false;
                if (isDecreasing == false)
                    yield return new WaitForSeconds(2);
                isDecreasing = true;
                
                if (textToFade.color.a >= 0 && !isIncreasing)
                {
                    varToDecrease.a -= fadeSpeed;
                    textToFade.color = varToDecrease;
                    yield return new WaitForSeconds(0.15f);

                    if (textToFade.color.a <= 0)
                    {
                        SceneManager.LoadSceneAsync(textToFade.text, LoadSceneMode.Single);
                        yield break;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }  
    }
}
