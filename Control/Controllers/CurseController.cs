using System;
using UnityEngine;

public class CurseController : MonoBehaviour
{
    public event Action<bool> OnCursed;

    private System.Random randomiser = new System.Random();

    public bool isEternalCurse;

    private void Start()
    {
        if (isEternalCurse)
        {
            CurseForever();
        }
    }

    private void OnValidate()
    {
        if (isEternalCurse)
        {
            CurseForever();
        }
    }

    public void RandomiseCurse()
    {
        if (!isEternalCurse)
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
        }
    }

    private void CurseForever()
    {
        OnCursed?.Invoke(true);
    }
}