using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class DialogueData : ScriptableObject
{
    public string additionalData;
    public Sprite[] emotions;
    [HideInInspector] public string[] dialogue;
    
    [SerializeField] private TextAsset textData;

    public void InitialiseDialogue()
    {
        if (textData != null)
        {
            List<string> rawData = new List<string>();
            rawData.Add(textData.ToString());
        
            dialogue = rawData.Last().Split('\n');
        }
    }
}