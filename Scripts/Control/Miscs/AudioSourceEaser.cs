using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

public class AudioSourceEaser : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float volumeDestination = 1;

    [Header("Behaviour")]
    [Tooltip("If set to false it will decrease")]
    [SerializeField]
    private bool isIncrease;

    [Space]
    [SerializeField]
    private bool isChangeValueOnStart;
    [ShowIf("isChangeValueOnStart")]
    [SerializeField]
    private float startValue;

    [Space]
    [ShowIf("isIncrease")]
    [SerializeField]
    private bool isDecreaseBeforeClipEnd;
    [ShowIf("isDecreaseBeforeClipEnd")]
    [SerializeField]
    private float beforeHowManySeconds;

    [Header("When to ease")]
    [SerializeField]
    private bool isOnStart;
    [SerializeField]
    private bool isOnEventEnd;

    [ShowIf("isOnEventEnd")]
    [Header("Event")]
    [SerializeField]
    private bool isWithFadeScreen;
    [ShowIf("isOnEventEnd")]
    [SerializeField]
    private bool isWithMainMenu;

    [Space]
    [ShowIf("isWithMainMenu")]
    [SerializeField]
    private MainMenu mainMenuManager;

    private void Start()
    {
        if (isChangeValueOnStart)
        {
            audioSource.volume = startValue;
        }

        if (isDecreaseBeforeClipEnd)
        {
            StartCoroutine(CheckSoundEnd());
        }

        if (isOnStart)
        {
            Ease();
        }

        if (isOnEventEnd)
        {
            if (isWithFadeScreen)
            {
                FadeScreenManager.Instance.OnEndFading += Ease;
            }
            else if (isWithMainMenu)
            {
                mainMenuManager.OnPlay += Ease;
            }
        }
    }
    private void OnDestroy()
    {
        if (isOnEventEnd)
        {
            if (isWithFadeScreen)
            {
                FadeScreenManager.Instance.OnEndFading -= Ease;
            }
            else if (isWithMainMenu)
            {
                mainMenuManager.OnPlay -= Ease;
            }
        }
    }

    private void Ease()
    {
        if (isIncrease)
        {
            IncreaseSound(speed);
        }
        else
        {
            DecreaseSound(speed);
        }
    }

    private void DecreaseSound(float speed, Action action = null)
    {
        Tween ease = DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0, speed).OnComplete(() => action?.Invoke());
        Debug.Log("decra");
    }
    private void IncreaseSound(float speed)
    {
        Tween ease = DOTween.To(() => audioSource.volume, x => audioSource.volume = x, volumeDestination, speed);
        Debug.Log("incra");
    }

    private IEnumerator CheckSoundEnd()
    {
        float length = audioSource.clip.length;

        float time = length - beforeHowManySeconds;

        yield return new WaitForSeconds(time);

        if (audioSource.loop)
        {
            DecreaseSound(beforeHowManySeconds, () =>
            {
                Ease();
                StartCoroutine(CheckSoundEnd());
            });
        }
        else
        {
            DecreaseSound(beforeHowManySeconds);
        }

        yield break;
    }
}