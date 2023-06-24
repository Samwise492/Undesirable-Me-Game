using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float freezeDuration;

    private Text textToFade => GetComponent<Text>();
    private Color defaultColor => textToFade.color;

    private bool isIncreasing = true;
    private bool isDecreasing = false;

    private void Start() => StartCoroutine(Fade());

    private IEnumerator Fade()
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
                        SceneManager.LoadSceneAsync(textToFade.text, LoadSceneMode.Single);

                        yield break;
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }  
    }
}