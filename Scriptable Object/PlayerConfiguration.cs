using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class PlayerConfiguration : ScriptableObject
{
    [HideInInspector] public UnityEvent OnPointsAdded;

    public int BadPoints => badPoints;
    public int GoodPoints => goodPoints;
    public int DeathPoints => deathPoints;

    public bool isKeyMinus_1;
    public bool isKey_0;
    public bool isKey_1;
    public bool isKey_2;
    public bool isKey_3;
    public bool isKey_4;

    [Space][SerializeField]
    private int badPoints;
    [SerializeField]
    private int goodPoints;
    [SerializeField]
    private int deathPoints;

    public bool CheckKey(int key)
    {
        switch (key)
        {
            case -1:
                return isKeyMinus_1;
            case 0:
                return isKey_0;
            case 1:
                return isKey_1;
            case 2:
                return isKey_2;
            case 3:
                return isKey_3;
            case 4:
                return isKey_4;
        }

        return false;
    }
    public void ChangeKey(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            switch (numbers[i])
            {
                case -1:
                    isKeyMinus_1 = true;
                    break;
                case 0:
                    isKey_0 = true;
                    break;
                case 1:
                    isKey_1 = true;
                    break;
                case 2:
                    isKey_2 = true;
                    break;
                case 3:
                    isKey_3 = true;
                    break;
                case 4:
                    isKey_4 = true;
                    break;
            }
        }
    }

    public void AddPoints(PointsToAddAfterDialogue[] pointsData)
    {
        foreach (PointsToAddAfterDialogue data in pointsData)
        {
            PointsType pointsType = data.PointsType;
            int quantity = data.Quantity;

            switch (pointsType)
            {
                case PointsType.BadPoints:
                    badPoints += quantity;
                    break;
                case PointsType.GoodPoints:
                    goodPoints += quantity;
                    break;
                case PointsType.DeathPoints:
                    deathPoints += quantity;
                    break;
            }
        }

        OnPointsAdded?.Invoke();
    }
    public void ResetPoints()
    {
        badPoints = 0;
        goodPoints = 0;
        deathPoints = 0;
    }

    public enum PointsType
    {
        BadPoints,
        GoodPoints,
        DeathPoints
    }
}