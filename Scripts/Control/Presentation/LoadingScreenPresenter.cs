using System;
using UnityEngine;

public class LoadingScreenPresenter : MonoBehaviour
{
	public event Action OnTitleLoaded;

	public static LoadingScreenPresenter Instance => instance;

	[SerializeField]
	private FadingText fadeText;

    [SerializeField]
    private CanvasGroup fadeCanvas;

	private static LoadingScreenPresenter instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        fadeCanvas.alpha = 0f;

        fadeText.OnFadingEnded += RaiseEvent;
        fadeText.OnFadingStarted += ShowCanvas;
    }
    private void OnDestroy()
    {
		fadeText.OnFadingEnded -= RaiseEvent;
        fadeText.OnFadingStarted -= ShowCanvas;
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