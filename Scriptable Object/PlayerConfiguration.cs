using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class PlayerConfiguration : ScriptableObject
{
    [HideInInspector] public UnityEvent OnPointsAdded;

    public int BadPoints => badPoints;
    public int GoodPoints => goodPoints;
    public int DeathPoints => deathPoints;

    public bool isKeyMinus1;
    public bool isKey0;
    public bool isKey1;
    public bool isKey2;
    public bool isKey3;
    public bool isKey4;

    [Space][SerializeField]
    private int badPoints;
    [SerializeField]
    private int goodPoints;
    [SerializeField]
    private int deathPoints;

    public void ChangeKey(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            switch (numbers[i])
            {
                case -1:
                    isKeyMinus1 = true;
                    break;
                case 0:
                    isKey0 = true;
                    break;
                case 1:
                    isKey1 = true;
                    break;
                case 2:
                    isKey2 = true;
                    break;
                case 3:
                    isKey3 = true;
                    break;
                case 4:
                    isKey4 = true;
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