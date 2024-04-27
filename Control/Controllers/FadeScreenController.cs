using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenController : MonoBehaviour
{
    public event Action OnStartFading;
    public event Action OnFading;
    public event Action OnEndFading;
    public event Action<PackedSceneData> OnPreparedForNewScene;

    [SerializeField]
    private Image fadingScreen;

    [Space]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float pauseTime;

    private PackedSceneData sceneToLoadAfterData;

    public void FadeScreenInAndOut()
    {
        fadingScreen.gameObject.SetActive(true);
        Fade(false);
    }

    public void FadeScreenIntoNewScene(PackedSceneData sceneToLoadAfterData)
    {
        this.sceneToLoadAfterData = sceneToLoadAfterData;

        fadingScreen.gameObject.SetActive(true);
        Fade(true);
    }

    private void Fade(bool isOnlyFadeIn)
    {
        OnStartFading?.Invoke();

        Color fadedColour = new(fadingScreen.color.r, fadingScreen.color.g, fadingScreen.color.b, 0);
        Color dimmedColour = new(fadingScreen.color.r, fadingScreen.color.g, fadingScreen.color.b, 1);

        Sequence seq = DOTween.Sequence();

        Tween show = DOTween.To(() => fadingScreen.color, x => fadingScreen.color = x, dimmedColour, speed);
        Tween hide = DOTween.To(() => fadingScreen.color, x => fadingScreen.color = x, fadedColour, speed);

        if (isOnlyFadeIn)
        {
            Tween customShow = DOTween.To(() => fadingScreen.color, x => fadingScreen.color = x, dimmedColour, speed);

            customShow.onComplete += () => OnFading?.Invoke();

            customShow.Play().onComplete += EndFading;
        }
        else
        {
            show.onComplete += () => OnFading?.Invoke();

            seq.Append(show);
            seq.AppendInterval(pauseTime);
            seq.Append(hide).onComplete += EndFading;
        }
    }

    private void EndFading()
    {
        if (sceneToLoadAfterData != null)
        {
            OnPreparedForNewScene?.Invoke(sceneToLoadAfterData);
        }

        sceneToLoadAfterData = null;

        OnEndFading?.Invoke();
    }
}