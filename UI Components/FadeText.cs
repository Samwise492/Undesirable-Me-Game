using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FadeText : MonoBehaviour
{
    public event Action OnFadingStarted;
    public event Action OnFadingEnded;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float pauseTime;

    private Text textToFade => GetComponent<Text>();

    public void Show(string textToShow)
    {
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);
        textToFade.text = textToShow;

        ProcessFading();
    }

    private void ProcessFading()
    {
        OnFadingStarted?.Invoke();

        Color fadedColour = new(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);
        Color dimmedColour = new(textToFade.color.r, textToFade.color.g, textToFade.color.b, 1);

        Sequence seq = DOTween.Sequence();

        Tween show = DOTween.To(() => textToFade.color, x => textToFade.color = x, dimmedColour, speed);
        Tween hide = DOTween.To(() => textToFade.color, x => textToFade.color = x, fadedColour, speed);

        seq.Append(show);
        seq.AppendInterval(pauseTime);
        seq.Append(hide).onComplete += () => OnFadingEnded?.Invoke();
    }
}