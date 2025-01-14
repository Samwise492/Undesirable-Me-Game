using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenManager : MonoBehaviour
{
    public event Action<bool> OnStartFading; // bool stands for is fading for new scene or not
    public event Action OnFading;
    public event Action OnEndFading;

    public static FadeScreenManager Instance {  get; private set; }

    [SerializeField]
    private Image fadingScreen;

    [Space]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float pauseTime;

    private float defaultPauseTime;
    private bool isPauseTimeOverrided;

    private PackedSceneData sceneToLoadAfterData;

    private void Awake()
    {
        Instance = this;

        defaultPauseTime = pauseTime;
    }

    public void OverridePauseTime(float value)
    {
        pauseTime = value;
        isPauseTimeOverrided = true;
    }

    public void FadeScreenInAndOut()
    {
        if (fadingScreen.gameObject.activeSelf)
        {
            return;
        }
        
        fadingScreen.gameObject.SetActive(true);
        Fade(false, false);
    }

    public void FadeScreenOut()
    {
        if (fadingScreen.gameObject.activeSelf)
        {
            return;
        }
        
        fadingScreen.gameObject.SetActive(true);
        Fade(false, true);
    }

    public void FadeScreenIntoNewScene(PackedSceneData sceneToLoadAfterData)
    {
        this.sceneToLoadAfterData = sceneToLoadAfterData;

        FadeScreenIn();
    }

    [ContextMenu("FadeInAndOut")]
    private void FadeScreenInAndOut_Debug()
    {
        FadeScreenInAndOut();
    }
    [ContextMenu("FadeOut")]
    private void FadeScreenOut_Debug()
    {
        FadeScreenOut();
    }
    [ContextMenu("FadeIn")]
    private void FadeScreenIn_Debug()
    {
        FadeScreenIn();
    }

    private void FadeScreenIn()
    {
        if (fadingScreen.gameObject.activeSelf)
        {
            return;
        }
        
        fadingScreen.gameObject.SetActive(true);
        Fade(true, false);
    }

    private void Fade(bool isOnlyFadeIn, bool isOnlyFadeOut)
    {
        OnStartFading?.Invoke(isOnlyFadeIn);

        Color fadedColour = new(fadingScreen.color.r, fadingScreen.color.g, fadingScreen.color.b, 0);
        Color dimmedColour = new(fadingScreen.color.r, fadingScreen.color.g, fadingScreen.color.b, 1);

        Sequence seq = DOTween.Sequence();

        Tween show = DOTween.To(() => fadingScreen.color, x => fadingScreen.color = x, dimmedColour, speed);
        Tween hide = DOTween.To(() => fadingScreen.color, x => fadingScreen.color = x, fadedColour, speed);

        if (isOnlyFadeIn)
        {
            fadingScreen.color = new Color(fadingScreen.color.r, fadingScreen.color.g, fadingScreen.color.b, 0);

            Tween customShow = DOTween.To(() => fadingScreen.color, x => fadingScreen.color = x, dimmedColour, speed);

            customShow.onComplete += () => OnFading?.Invoke();

            customShow.Play().onComplete += EndFading;

            return;
        }
        if (isOnlyFadeOut)
        {
            fadingScreen.color = new Color(fadingScreen.color.r, fadingScreen.color.g, fadingScreen.color.b, 1);

            Tween customShow = DOTween.To(() => fadingScreen.color, x => fadingScreen.color = x, fadedColour, speed);

            customShow.onComplete += () => OnFading?.Invoke();

            customShow.Play().onComplete += EndFading;

            return;
        }
        else
        {
            fadingScreen.color = new Color(fadingScreen.color.r, fadingScreen.color.g, fadingScreen.color.b, 0);

            show.onComplete += () => OnFading?.Invoke();

            seq.Append(show);
            seq.AppendInterval(pauseTime);
            seq.Append(hide).onComplete += EndFading;

            return;
        }
    }

    private void EndFading()
    {
        if (sceneToLoadAfterData != null)
        {
            LoadingManager.Instance.LoadScene(sceneToLoadAfterData, false);
        }

        sceneToLoadAfterData = null;

        if (isPauseTimeOverrided)
        {
            ReturnPauseTimeToDefault();
        }

        OnEndFading?.Invoke();
    }

    private void ReturnPauseTimeToDefault()
    {
        pauseTime = defaultPauseTime;
        isPauseTimeOverrided = false;
    }
}