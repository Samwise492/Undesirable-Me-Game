using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

[CreateAssetMenu]
public class DialogueData : ScriptableObject
{
    public int[] KeyData => keyData;
    public PointsToAddAfterDialogue[] PointsData => pointsData;
    public DialogueLineEmotion[] Emotions => emotions;
    public string[] Dialogue => dialogue;

    public bool isFinished;

    [SerializeField]
    private int[] keyData;
    [SerializeField]
    private PointsToAddAfterDialogue[] pointsData;
    [SerializeField]
    private DialogueLineEmotion[] emotions;
    [SerializeField]
    private LocalisationTextData[] textData;

    [Header("Debug Info")]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    [TextArea(1, 50)]
    [SerializeField]
    private string[] dialogue;

    public void InitialiseDialogue()
    {
        TextAsset textAsset = GetTextAssetByLanguage();

        if (textAsset != null)
        {
            List<string> rawData = new List<string>
            {
                textAsset.ToString()
            };

            dialogue = rawData.Last().Split('\n');
        }
    }

    private void OnValidate()
    {
        if (textData.Length > 0)
        {
            InitialiseDialogue();
        }
    }

    private TextAsset GetTextAssetByLanguage()
    {
        switch (LocalizationSettings.SelectedLocale.LocaleName)
        {
            case "English (en)":
                return textData.Where(x => x._Language == LocalisationTextData.Language.English).First().TextData;
            case "Russian (ru)":
                return textData.Where(x => x._Language == LocalisationTextData.Language.Russian).First().TextData;
        }

        return null;
    }
}

[Serializable]
public class DialogueLineEmotion
{
    public Characters character;
    public Moods mood;
}

[Serializable]
public class LocalisationTextData
{
    public Language _Language => language;
    public TextAsset TextData => textData;

    [SerializeField]
    private Language language;
    [SerializeField]
    private TextAsset textData;

    public enum Language
    {
        English,
        Russian
    }
}

[Serializable]
public class PointsToAddAfterDialogue
{
    public int Quantity { get => quantity; }
    public PlayerConfiguration.PointsType PointsType { get => pointsType; }

    public PointsToAddAfterDialogue(PlayerConfiguration.PointsType pointsType, int quantity)
    {
        this.pointsType = pointsType;
        this.quantity = quantity;
    }

    [SerializeField]
    private PlayerConfiguration.PointsType pointsType;
    [SerializeField]
    private int quantity;
}