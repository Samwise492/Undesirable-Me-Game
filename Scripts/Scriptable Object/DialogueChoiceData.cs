using System;
using UnityEngine;

[CreateAssetMenu]
public class DialogueChoiceData : ScriptableObject
{
    public LocalisedChoiceLine[] ChoiceLines => choiceLines;
    public DialogueData DialogueData => dialogueData;

    [SerializeField]
    private LocalisedChoiceLine[] choiceLines;

    [SerializeField]
    private DialogueData dialogueData;
}

[Serializable]
public class LocalisedChoiceLine
{
    public Language Language => language;
    public string Line => line;

    [SerializeField]
    private Language language;
    [SerializeField]
    private string line;
}
