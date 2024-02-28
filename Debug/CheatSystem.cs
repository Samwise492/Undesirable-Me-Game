using UnityEngine;

public class CheatSystem : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private bool isPlayerCustomisable;
    [SerializeField]
    private float customPlayerSpeed;

    [Header("Doors")]
    [SerializeField] 
    private bool areDoorsUnlocked;

    [Header("Player Points")]
    [SerializeField] 
    private bool isPointsReset;
    [SerializeField] 
    private PlayerConfiguration playerConfiguration;
    
    private SpecificDoorBehaviour[] doors;

    private bool areDoorsUnlocked_atStart;

    private string cheatTag = $"<color=red>[Cheats]</color>";

    private void Awake()
    {
        areDoorsUnlocked_atStart = areDoorsUnlocked;
    }
    private void Start()
    {
        if (isPointsReset)
        {
            playerConfiguration.ResetPoints();
            print($"{cheatTag} <i>Player points have been reset</i>");
        }
    }
    private void OnValidate()
    {
        if (Application.isPlaying) 
        {
            if (areDoorsUnlocked != areDoorsUnlocked_atStart)
            {
                doors = FindObjectsOfType<SpecificDoorBehaviour>(true);

                foreach (SpecificDoorBehaviour door in doors)
                {
                    door.isLocked = !areDoorsUnlocked;
                    print($"{cheatTag} <i>Door has been cracked.</i>");
                }
            }

            if (isPlayerCustomisable)
            {
                FindObjectOfType<Player>().moveSpeed = customPlayerSpeed;
            }
        }
    }
}