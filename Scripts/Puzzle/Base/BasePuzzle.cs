using System;
using UnityEngine;

public class BasePuzzle : MonoBehaviour
{
    public event Action OnPuzzleStarted;
    public event Action OnPuzzleEnded;

    public bool isDebugOnStart;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private bool isAudioOnEnd = true;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip correctSound;
    [SerializeField]
    private AudioClip incorrectSound;


    protected virtual void Start()
    {
#if UNITY_EDITOR == false
        isDebugOnStart = false;
#elif DEVELOPMENT_BUILD
        isDebugOnStart = true;
#endif

        if (isDebugOnStart)
        {
            StartPuzzle();
            Cursor.visible = true;
        }
    }

    public void SetPuzzleVisibility(bool state)
    {
        canvasGroup.alpha = state ? 1 : 0;

        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }

    public void NotifyIncorrect()
    {
        audioSource.clip = incorrectSound;
        audioSource.Play();
    }

    public virtual void StartPuzzle()
    {
        OnPuzzleStarted?.Invoke();
    }
    public virtual void EndPuzzle()
    {
        if (isAudioOnEnd)
        {
            audioSource.clip = correctSound;
            audioSource.Play();
        }

        OnPuzzleEnded?.Invoke();
    }
}