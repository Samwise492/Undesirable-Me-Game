using System.Globalization;
using UnityEngine;

public class PlayerSettingsFileManager : BasePersistentDataFileManager<PlayerSettings>
{
    protected override void Awake()
    {
        base.Awake();

        fileName = "PlayerSettings";
    }

    protected override PlayerSettings InitFirstSave()
    {
        PlayerSettings newData = new()
        {
            isMusicMuted = false,
            isSFXMuted = false,

            musicVolume = 0,
            sfxVolume = 0,

            displayMode = FullScreenMode.FullScreenWindow,
            displayResolutionX = 1920,
            displayResolutionY = 1080,

            language = GetLanguageByRegion()
        };

        return newData;
    }

    private Language GetLanguageByRegion()
    {
        string currentRegion = RegionInfo.CurrentRegion.DisplayName;
        Debug.Log("region " + currentRegion);

        switch (currentRegion)
        {
            case "RU":
                return Language.Russian;
            default:
                return Language.English;
        }
    }
}