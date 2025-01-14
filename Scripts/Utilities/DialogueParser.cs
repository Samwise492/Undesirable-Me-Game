using System.Collections.Generic;
using System.Linq;
using UnityEngine.Localization.Settings;

public static class DialogueParser
{
    public static string FormatDialogue(string line)
    {
        List<string> rawWords = line.Split().ToList();
        List<string> clearedWords = new();

        for (int i = 0; i < rawWords.Count; i++)
        {
            char[] charSet = rawWords[i].ToCharArray();

            for (int j = 0; j < charSet.Length; j++)
            {
                if (charSet[j] == '!' || charSet[j] == ';' || charSet[j] == ',' ||
                   charSet[j] == ':' || charSet[j] == '?' || charSet[j] == '.' ||
                   charSet[j] == '\'')
                {
                    charSet = charSet.Where(x => x != charSet[j]).ToArray();
                }
            }

            clearedWords.Add(new string(charSet));
        }

        IEnumerable<string> names = clearedWords.Select(i => i.ToString()).Intersect(GetNamesByLocalisation().Keys);

        string newLine = line;

        foreach (string name in names)
        {
            newLine = newLine.Replace(name, GetNamesByLocalisation()[name]);
        }

        return newLine;
    }

    private static Dictionary<string, string> GetNamesByLocalisation()
    {
        Dictionary<string, string> dict = new();

        if (PlayerSettingsFileManager.Instance.LoadData().language == Language.English)
        {
            dict = CharacterNameData.englishCharacterNames;
        }
        else if (PlayerSettingsFileManager.Instance.LoadData().language == Language.Russian)
        {
            dict = CharacterNameData.russianCharacterNames;
        }

        return dict;
    }
}