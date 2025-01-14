using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequence : MonoBehaviour
{
    public event Action OnSequenceStarted;
    public event Action OnSequenceEnded;

    public bool isDebug;

    [SerializeField]
    private List<BasePuzzle> sequence;

    private int currentPuzzleIndex;
    private bool isPuzzleSequenceProcessing;

    private void Start()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            int index = i;

            sequence[i].SetPuzzleVisibility(false);

            sequence[i].OnPuzzleEnded += FadeScreenManager.Instance.FadeScreenInAndOut;
            sequence[i].OnPuzzleEnded += delegate { CheckForSequenceEnd(index); };
        }

        FadeScreenManager.Instance.OnFading += HideCurrentPuzzle;
        FadeScreenManager.Instance.OnFading += ShowNewPuzzle;
        FadeScreenManager.Instance.OnEndFading += PlayPuzzle;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        isDebug = true;
#elif UNITY_EDITOR == false
        isDebug = false;
#endif
    }
    private void OnDestroy()
    {
        for (int i = 0; i < sequence.Count - 1; i++)
        {
            int index = i;

            sequence[i].OnPuzzleEnded -= FadeScreenManager.Instance.FadeScreenInAndOut;
            sequence[i].OnPuzzleEnded -= delegate { CheckForSequenceEnd(index); };
        }

        FadeScreenManager.Instance.OnFading -= HideCurrentPuzzle;
        FadeScreenManager.Instance.OnFading -= ShowNewPuzzle;
        FadeScreenManager.Instance.OnEndFading -= PlayPuzzle;
    }
    private void Update()
    {
        if (isDebug)
        {
            if (Input.GetKeyDown(InputData.debugKey_Two))
            {
                if (currentPuzzleIndex < sequence.Count - 1)
                {
                    HideCurrentPuzzle();
                    ShowNewPuzzle();
                    PlayPuzzle();
                }
                else
                {
                    CheckForSequenceEnd(currentPuzzleIndex);
                }
            }
        }
    }

    public void StartPuzzleSequence()
    {
        CursorManager.Instance.SetCursor(true);
        isPuzzleSequenceProcessing = true;

        ShowNewPuzzle();
        sequence[0].StartPuzzle();

        OnSequenceStarted?.Invoke();
    }

    private void HideCurrentPuzzle()
    {
        if (isPuzzleSequenceProcessing)
        {
            sequence[currentPuzzleIndex].SetPuzzleVisibility(false);

            currentPuzzleIndex++;
        }
    }

    private void ShowNewPuzzle()
    {
        if (isPuzzleSequenceProcessing)
        {
            sequence[currentPuzzleIndex].SetPuzzleVisibility(true);
        }
    }

    private void PlayPuzzle()
    {
        if (isPuzzleSequenceProcessing)
        {
            sequence[currentPuzzleIndex].StartPuzzle();
        }
    }

    private void CheckForSequenceEnd(int index)
    {
        if (index == sequence.Count - 1)
        {
            HideCurrentPuzzle();
            isPuzzleSequenceProcessing = false;

            OnSequenceEnded?.Invoke();
        }
    }
}