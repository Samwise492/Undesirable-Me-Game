using System.Linq;
using UnityEngine;

public class PlayerSaveLoadProvider : MonoBehaviour
{
    public static PlayerSaveLoadProvider Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public PlayerSaveStorage.PlayerSave GetCurrentSave()
    {
        return TempPlayerSave.Instance.Save;
    }
    public void SaveInTemp(PlayerSaveStorage.PlayerSave save)
    {
        TempPlayerSave.Instance.Save = save;
    }
    public void SaveTempByIndex(int index, PlayerSaveStorage.PlayerSave save)
    {
        PlayerSaveStorage newSaves = PlayerSaveFileManager.Instance.LoadData();

        if (newSaves.saves.ContainsKey(index))
        {
            newSaves.saves[index] = save;
        }
        else
        {
            newSaves.saves.Add(index, save);
        }

        PlayerSaveFileManager.Instance.SaveData(newSaves);
    }

    public PlayerSaveStorage.PlayerSave GatherSaveInfo()
    {
        Player player = FindObjectOfType<Player>();

        TempPlayerSave.Instance.Save.playerPositionX = player.transform.localPosition.x;
        TempPlayerSave.Instance.Save.playerPositionY = player.transform.localPosition.y;
        TempPlayerSave.Instance.Save.playerPositionZ = player.transform.localPosition.z;

        TempPlayerSave.Instance.Save.stageDay = StageManager.Instance.GetCurrent().DayNumber;
        TempPlayerSave.Instance.Save.stage = StageManager.Instance.GetCurrent().Stages.Where(x => x.objectLink.activeInHierarchy).First().name;

        TempPlayerSave.Instance.Save.passedDialogueNumbers = GetCurrentSave().passedDialogueNumbers;//LogDataManager.Instance.GetAllProgressedDialogueIndices();

        PackedSceneData packedSceneData = new()
        {
            sceneToLoad = LoadingManager.Instance.LastReceivedData.sceneToLoad,
            stageDayToLoad = TempPlayerSave.Instance.Save.stageDay,
            stageToLoad = TempPlayerSave.Instance.Save.stage,
            isLoadingScreenRequired = false,
        };

        TempPlayerSave.Instance.Save.lastPackedSceneData = packedSceneData;

        return TempPlayerSave.Instance.Save;
    }
}
