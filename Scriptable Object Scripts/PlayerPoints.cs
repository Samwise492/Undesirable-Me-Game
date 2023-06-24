using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class PlayerPoints : ScriptableObject
{
    [HideInInspector] public UnityEvent OnPointsAdded;

    public int badPoints;
    public int goodPoints;
    public int deathPoints;

    public void AddPoints(PointsType pointsType, int quantity)
    {
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
