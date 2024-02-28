using System;
using UnityEngine;

public class CurseController : MonoBehaviour
{
    public event Action<bool> OnCursed;

    private System.Random randomiser = new System.Random();

    [Header("Debug")]
    [SerializeField]
    private bool isDebugCurseState;

    public void RandomiseCurse()
    {
        int randomValue = randomiser.Next(4);

        if (randomValue == 1)
        {
            OnCursed?.Invoke(true);
        }
        else
        {
            OnCursed?.Invoke(false);
        }

        // Debug

        if (isDebugCurseState)
        {
            OnCursed?.Invoke(true);
        }
    }
}