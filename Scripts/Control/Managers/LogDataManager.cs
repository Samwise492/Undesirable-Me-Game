using System;
using System.Collections.Generic;
using UnityEngine;

public class LogDataManager : MonoBehaviour
{
    public event Action OnHistoryChanged;

    public static LogDataManager Instance => instance;

    public bool isDebug;

    [SerializeField]
    private DialogueLibrary library;

    private static LogDataManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Dictionary<int, string[]> GetLogs()
    {
        Dictionary<int, string[]> logs = new();

        for (int i = 0; i < GetPassedDialogues().Count; i++)
        {
            GetPassedDialogues()[i].InitialiseDialogue();
            logs.Add(i, GetPassedDialogues()[i].Dialogue);
        }

        return logs;
    }

    public void SaveToHistory(DialogueData data)
    {
        if (!library.Dialogues.Contains(data))
        {
            return;
        }

        if (!GetPassedDialogues().Contains(data))
        {
            PlayerSaveStorage.PlayerSave newSave = new();

            newSave = PlayerSaveLoadProvider.Instance.GetCurrentSave();
            newSave.passedDialogueNumbers.Add(library.Dialogues.IndexOf(data));

            PlayerSaveLoadProvider.Instance.SaveInTemp(newSave);

            OnHistoryChanged?.Invoke();
        }
    }

    public void EraseHistory()
    {
        PlayerSaveStorage.PlayerSave newData = PlayerSaveLoadProvider.Instance.GetCurrentSave();

        newData.passedDialogueNumbers.Clear();

        PlayerSaveLoadProvider.Instance.SaveInTemp(newData);
    }

    public List<DialogueData> GetPassedDialogues()
    {
        List<DialogueData> passedDialogues = new();

        foreach (int i in PlayerSaveLoadProvider.Instance.GetCurrentSave().passedDialogueNumbers)
        {
            passedDialogues.Add(library.Dialogues[i]);
        }

        if (isDebug)
        {
            foreach (DialogueData data in passedDialogues)
            {
                print(data.name);
            }
        }

        return passedDialogues;
    }
}