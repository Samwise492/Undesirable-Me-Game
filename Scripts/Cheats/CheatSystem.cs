#if UNITY_EDITOR || DEVELOPMENT_BUILD
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class CheatSystem : MonoBehaviour
{
    [SerializeField]
    private bool isTurnOffCheats;

    [Header("Localisation")]
    [SerializeField]
    private bool isLocalisationCustomasible;
    [ShowIf("isLocalisationCustomasible")]
    [SerializeField]
    private UnityEngine.Localization.Locale languageToSet;

    [Header("Player")]
    [SerializeField]
    private bool isPlayerCustomisable;
    [ShowIf("isPlayerCustomisable")]
    [SerializeField]
    private float customPlayerSpeed;

    [Header("Doors")]
    [SerializeField]
    private bool areDoorsUnlocked;

    [Header("Player Save")]
    [SerializeField]
    private bool isPointsReset;
    [SerializeField]
    private bool isEraseAllDialoguesOnStart;

    [Header("Dialogue")]
    [SerializeField]
    private bool isDialogueCustomisable;
    [ShowIf("isDialogueCustomisable")]
    [SerializeField]
    private float customDialogueDelay;

    private SpecificDoorBehaviour[] doors;
    private BaseDialogue[] dialogues;

    private bool areDoorsUnlocked_atStart;

    private string cheatTag = $"<color=red>[Cheats]</color>";

    private void Awake()
    {
        if (!isTurnOffCheats)
        {
            areDoorsUnlocked_atStart = areDoorsUnlocked;
        }
    }
    private void Start()
    {
        if (!isTurnOffCheats)
        {
            if (isPointsReset)
            {
                PlayerProgressProvider.Instance.ResetPoints();
                print($"{cheatTag} <i>Player points have been reset in the save file</i>");
            }

            if (isEraseAllDialoguesOnStart)
            {
                PlayerProgressProvider.Instance.EraseAllDialogues();
                print($"{cheatTag} <i>Dialogues have been erased from the save file</i>");
            }

            if (isLocalisationCustomasible)
            {
                LocalizationSettings.SelectedLocale = languageToSet;
                SaveLanguage();
                print($"{cheatTag} <i>Localisation has been set to {languageToSet.ToString().Split(' ')[0]}</i>");
            }
        }
    }
    private void OnValidate()
    {
        if (isTurnOffCheats)
        {
            return;
        }

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
                Invoke(nameof(ChangePlayer), 0.5f);
            }

            if (isDialogueCustomisable)
            {
                Constants.DelayBetweenDialogueLines = customDialogueDelay;
            }
        }
    }

    private Language GetLanguage()
    {
        switch (LocalizationSettings.SelectedLocale.LocaleName)
        {
            case "Russian (ru)":
                return Language.Russian;
            default:
                return Language.English;
        }
    }
    private void SaveLanguage()
    {
        PlayerSettings playerSettings = PlayerSettingsFileManager.Instance.LoadData();
        playerSettings.language = GetLanguage();
        PlayerSettingsFileManager.Instance.SaveData(playerSettings);
    }

    private void ChangePlayer()
    {
        Player player = FindObjectOfType<Player>();

        if (player)
        {
            player.moveSpeed = customPlayerSpeed;
        }
    }
}
#endif