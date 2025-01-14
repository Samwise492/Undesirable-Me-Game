using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class DialogueData : ScriptableObject
{
    public int[] KeyData => keyData;
    public PointsToAddAfterDialogue[] PointsData => pointsData;
    public DialogueLineEmotion[] Emotions => emotions;
    public string[] Dialogue => dialogue;

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

    private List<string[]> InitialiseDialogue_Debug()
    {
        List<TextAsset> textAssets = new();
        List<string[]> debugDialogues = new();

        foreach (LocalisationTextData data in textData)
        {
            textAssets.Add(data.TextData);
        }

        if (textAssets.Count > 0)
        {
            foreach (TextAsset asset in textAssets)
            {
                List<string> rawData = new List<string>
                {
                    asset.ToString()
                };

                string[] raw = rawData.Last().Split('\n');
                debugDialogues.Add(raw);
            }
        }

        return debugDialogues;
    }

    private void OnValidate()
    {
        if (textData.Length > 0)
        {
            InitialiseDialogue();

            foreach (string[] debugDialogue in InitialiseDialogue_Debug())
            {
                if (debugDialogue.Length != emotions.Length)
                {
                    Debug.LogError($"Length of dialogue on {name} is not equal to emotion one.");
                }
            }
        }
    }

    private TextAsset GetTextAssetByLanguage()
    {
        if (PlayerSettingsFileManager.Instance)
        {
            switch (PlayerSettingsFileManager.Instance.LoadData().language)
            {
                case Language.Russian:
                    return textData.Where(x => x.Language == Language.Russian).First().TextData;
                default:
                    return textData.Where(x => x.Language == Language.English).First().TextData;
            }
        }

        return textData.Where(x => x.Language == Language.English).First().TextData;
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
    public Language Language => language;
    public TextAsset TextData => textData;

    [SerializeField]
    private Language language;
    [SerializeField]
    private TextAsset textData;
}

public enum Language
{
    English,
    Russian
}

[Serializable]
public class PointsToAddAfterDialogue
{
    public int Quantity { get => quantity; }
    public StoryPointsType PointsType { get => pointsType; }

    public PointsToAddAfterDialogue(StoryPointsType pointsType, int quantity)
    {
        this.pointsType = pointsType;
        this.quantity = quantity;
    }

    [SerializeField]
    private StoryPointsType pointsType;
    [SerializeField]
    private int quantity;
}