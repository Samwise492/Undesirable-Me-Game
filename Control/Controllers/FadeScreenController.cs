using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenController : MonoBehaviour
{
    public event Action OnStartFading;
    public event Action<PackedSceneData> OnEndFading;

    [SerializeField]
    private float fadeSpeed;
    [SerializeField]
    private Image fadingScreen;

    private PackedSceneData sceneToLoadAfterData;

    public void FadeScreen(PackedSceneData sceneToLoadAfterData)
    {
        this.sceneToLoadAfterData = sceneToLoadAfterData;

        fadingScreen.gameObject.SetActive(true);

        StartCoroutine(ProcessFadeScreen());
    }

    private IEnumerator ProcessFadeScreen()
    {
        yield return new WaitForSeconds(1);

        OnStartFading?.Invoke();

        Color varToDecrease = fadingScreen.color;

        while (true)
        {
            varToDecrease.a += fadeSpeed;
            fadingScreen.color = varToDecrease;
            yield return new WaitForSeconds(0.15f);

            if (fadingScreen.color.a >= 1)
            {
                OnEndFading?.Invoke(sceneToLoadAfterData);

                yield break;
            }
        }
    }
}
