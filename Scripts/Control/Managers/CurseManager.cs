using System;
using UnityEngine;

public class CurseManager : MonoBehaviour
{
    public static CurseManager Instance { get; private set; }

    public bool isWorldCursed;
    public bool isEternalCurse;

    private System.Random randomiser = new System.Random();

    private void Awake()
    {
        Instance = this;
    }
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
                isWorldCursed = true;
            }
            else
            {
                isWorldCursed = false;
            }
        }
    }

    private void CurseForever()
    {
        isWorldCursed = true;
    }
}