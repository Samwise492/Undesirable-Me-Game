using System;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
	public event Action OnTitleLoaded;

	public static LoadingScreenController Instance => instance;

	[SerializeField]
	private FadeText fadeText;

    [SerializeField]
    private CanvasGroup fadeCanvas;

	private static LoadingScreenController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        fadeCanvas.alpha = 0f;


        fadeText.OnFadeEnded += RaiseEvent;
        fadeText.OnFade += ShowCanvas;
    }
    private void OnDestroy()
    {
		fadeText.OnFadeEnded -= RaiseEvent;
        fadeText.OnFade -= ShowCanvas;
    }

    public void ShowLoadingTitle(string text)
	{
		fadeText.Show(text);
    }

    private void ShowCanvas()
    {
        fadeCanvas.alpha = 1f;
    }

	private void RaiseEvent()
	{
		OnTitleLoaded?.Invoke();
    }
}