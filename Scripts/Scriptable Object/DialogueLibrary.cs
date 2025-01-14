using System.Collections.Generic;
using UnityEngine;

public class DialogueLibrary : ScriptableObject
{
    public List<DialogueData> Dialogues => dialogues;

    [SerializeField]
    private List<DialogueData> dialogues;
}