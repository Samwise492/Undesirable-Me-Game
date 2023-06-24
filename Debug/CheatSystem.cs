using UnityEngine;

public class CheatSystem : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private bool isDoorUnlocked;

    [Header("Player Points")]
    [SerializeField] private bool isPointsReset;
    [SerializeField] private PlayerPoints playerPoints;
    
    private SpecificDoorBehaviour[] doors;

    private bool isDoorUnlocked_atStart;
    private string cheatTag = $"<color=red>[Cheats]</color>";

    private void Awake() => isDoorUnlocked_atStart = isDoorUnlocked;
    private void Start()
    {
        if (isPointsReset)
        {
            playerPoints.ResetPoints();
            print($"{cheatTag} <i>Player points have been reset</i>");
        }
    }
    private void OnValidate()
    {
        if (Application.isPlaying) 
        {
            if (isDoorUnlocked != isDoorUnlocked_atStart)
            {
                doors = Resources.FindObjectsOfTypeAll<SpecificDoorBehaviour>();

                foreach (SpecificDoorBehaviour door in doors)
                {
                    door.isLocked = !isDoorUnlocked;
                    print($"{cheatTag} <i>Door has been cracked.</i>");
                }
            }
        }
    }
}