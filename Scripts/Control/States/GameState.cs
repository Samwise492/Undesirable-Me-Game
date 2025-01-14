using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public CurrentGameState CurrentState { get; private set; }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public void TransitTo(CurrentGameState state)
    {
        CurrentState = state;
    }

    public void DelayedTransitTo(CurrentGameState state)
    {
        StartCoroutine(DelayTransition(state));
    }

    private IEnumerator DelayTransition(CurrentGameState state)
    {
        yield return new WaitForSeconds(0.5f);

        CurrentState = state;

        yield break;
    }
}

public enum CurrentGameState
{
    Default,
    IsLoadStageByProgress,
    IsLoadStageFromMenu
}