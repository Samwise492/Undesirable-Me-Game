using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FadeText : MonoBehaviour
{
    public event Action OnFadeEnded;
    public event Action OnFade;

    [SerializeField] 
    private float fadeSpeed;
    [SerializeField] 
    private float freezeDuration;

    private Text textToFade => GetComponent<Text>();

    private bool isIncreasing = true;
    private bool isDecreasing = false;

    public void Show(string textToShow)
    {
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);
        textToFade.text = textToShow;

        OnFade?.Invoke();

        StartCoroutine(ProcessFading());
    }

    private IEnumerator ProcessFading()
    {
        Color varToDecrease = textToFade.color;

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
                {
                    yield return new WaitForSeconds(freezeDuration);
                }

                isDecreasing = true;
                
                if (textToFade.color.a >= 0 && !isIncreasing)
                {
                    varToDecrease.a -= fadeSpeed;
                    textToFade.color = varToDecrease;

                    yield return new WaitForSeconds(0.15f);

                    if (textToFade.color.a <= 0)
                    {
                        transform.parent.gameObject.SetActive(false);

                        OnFadeEnded?.Invoke();

                        yield break;
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }  
    }
}