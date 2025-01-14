using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StampWindow : MonoBehaviour
{
    public event Action<bool> OnStampWindowStateChanged;

    [SerializeField]
    private CanvasGroup stampWindow;

    public Button realStampButton;
    public Button fakeStampButton;

    [Header("Hints")]
    [SerializeField]
    private float showSpeed;
    [Space]
    [SerializeField]
    private CanvasGroup realStampHint;
    [SerializeField]
    private CanvasGroup fakeStampHint;

    private bool isShowStarted;
    private bool isHidingStarted;

    private void Start()
    {
        realStampHint.alpha = 0;
        fakeStampHint.alpha = 0;

        SetInteractable(false);
    }

    public void SetInteractable(bool state)
    {
        stampWindow.interactable = state;

        CursorManager.Instance.SetCursor(state);
    }

    public bool GetStampWindowState()
    {
        return stampWindow.alpha == 1;
    }

    public void SetStampWindowState(bool state)
    {
        if (state && stampWindow.alpha == 1)
        {
            return;
        }
        
        stampWindow.alpha = state ? 1 : 0;

        OnStampWindowStateChanged?.Invoke(state);
    }

    public void HandleHintState(GameObject objectForHint)
    {
        if (stampWindow.interactable)
        {
            if (objectForHint == realStampButton.gameObject)
            {
                StartCoroutine(ProcessHintHiding(fakeStampHint));
                StartCoroutine(ProcessHintShowing(realStampHint));
            }
            else if (objectForHint == fakeStampButton.gameObject)
            {
                StartCoroutine(ProcessHintHiding(realStampHint));
                StartCoroutine(ProcessHintShowing(fakeStampHint));
            }
            else
            {
                StartCoroutine(ProcessHintHiding(fakeStampHint));
                StartCoroutine(ProcessHintHiding(realStampHint));
            }
        }
    }

    private IEnumerator ProcessHintShowing(CanvasGroup canvasGroup)
    {
        if (!isShowStarted && !isHidingStarted)
        {
            isShowStarted = true;

            while (canvasGroup.alpha < 1)
            {
                yield return new WaitForEndOfFrame();

                canvasGroup.alpha += showSpeed;
            }

            isShowStarted = false;

            yield break;
        }
        else
        {
            yield break;
        }
    }

    private IEnumerator ProcessHintHiding(CanvasGroup canvasGroup)
    {
        if (!isHidingStarted && !isShowStarted)
        {
            isHidingStarted = true;

            while (canvasGroup.alpha > 0)
            {
                yield return new WaitForEndOfFrame();

                canvasGroup.alpha -= showSpeed;
            }

            isHidingStarted = false;

            yield break;
        }
        else
        {
            yield break;
        }
    }
}
