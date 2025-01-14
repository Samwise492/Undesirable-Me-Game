using UnityEngine;

public class TempPlayerSave : MonoBehaviour
{
    public static TempPlayerSave Instance { get; private set; }

    public PlayerSaveStorage.PlayerSave Save;

    private void Awake()
    {
        Instance = this;

        CreateFirstSave();

        DontDestroyOnLoad(this);
    }

    private void CreateFirstSave()
    {
        Save = new()
        {
            passedDialogueNumbers = new(),
            passedTriggerGuids = new(),

            playerPositionX = 0,
            playerPositionY = 0,
            playerPositionZ = 0,

            stageDay = 1,
            stage = "Office",

            lastPackedSceneData = new()
            {
                sceneToLoad = "Hospital",
                stageDayToLoad = 1,
                stageToLoad = "Office",
                isLoadingScreenRequired = true,

                localisedDayText = null,
                localisedLocationText = null
            },

            isGreeted = true,

            progress = new()
            {
                playerKeys = new()
                {
                    { -1, false },
                    { 0, false },
                    { 1, false },
                    { 2, false },
                    { 3, false },
                    { 4, false },
                    { 5, false }
                },
                playerPoints = new()
                {
                    { StoryPointsType.BadPoints, 0 },
                    { StoryPointsType.GoodPoints, 0 },
                    { StoryPointsType.DeathPoints, 0 }
                }
            }
        };
    }
}
