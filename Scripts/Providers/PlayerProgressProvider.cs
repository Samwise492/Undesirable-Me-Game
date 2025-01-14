using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class PlayerProgressProvider : MonoBehaviour
{
    public event Action OnPointsAdded;

    public static PlayerProgressProvider Instance => instance;

    private static PlayerProgressProvider instance;

    private void Awake()
    {
        instance = this;
    }

    public bool GetKeyValue(int key)
    {
        return PlayerSaveLoadProvider.Instance.GetCurrentSave().progress.playerKeys[key];
    }
    public void SetKeyValue(int[] keysToActivate)
    {
        PlayerSaveStorage.PlayerSave save = PlayerSaveLoadProvider.Instance.GetCurrentSave();
        PlayerProgress newProgress = PlayerSaveLoadProvider.Instance.GetCurrentSave().progress;

        for (int i = 0; i < keysToActivate.Length; i++)
        {
            PlayerSaveLoadProvider.Instance.GetCurrentSave().progress.playerKeys[keysToActivate[i]] = true;
        }

        save.progress = newProgress;
        PlayerSaveLoadProvider.Instance.SaveInTemp(save);
    }

    public int GetPoints(StoryPointsType type)
    {
        return PlayerSaveLoadProvider.Instance.GetCurrentSave().progress.playerPoints.Where(x => x.Key == type).First().Value;
    }
    public void SetPoints(PointsToAddAfterDialogue[] pointsData)
    {
        PlayerSaveStorage.PlayerSave save = PlayerSaveLoadProvider.Instance.GetCurrentSave();
        PlayerProgress newProgress = PlayerSaveLoadProvider.Instance.GetCurrentSave().progress;

        foreach (PointsToAddAfterDialogue data in pointsData)
        {
            StoryPointsType pointsType = data.PointsType;
            int quantity = data.Quantity;

            newProgress.playerPoints[pointsType] += quantity;
        }

        save.progress = newProgress;
        PlayerSaveLoadProvider.Instance.SaveInTemp(save);

        StartCoroutine(CheckDeathEnding());

        OnPointsAdded?.Invoke();
    }
    public void ResetPoints()
    {
        PlayerSaveStorage.PlayerSave save = PlayerSaveLoadProvider.Instance.GetCurrentSave();
        PlayerProgress newProgress = new();

        Dictionary<StoryPointsType, int> newPlayerPoints = new();

        List<StoryPointsType> availableTypes = new(PlayerSaveLoadProvider.Instance.GetCurrentSave().progress.playerPoints.Keys);

        foreach (StoryPointsType key in availableTypes)
        {
            newPlayerPoints.Add(key, 0);
        }

        newProgress.playerKeys = PlayerSaveLoadProvider.Instance.GetCurrentSave().progress.playerKeys;
        newProgress.playerPoints = newPlayerPoints;

        save.progress = newProgress;
        PlayerSaveLoadProvider.Instance.SaveInTemp(save);
    }

    public void EraseAllDialogues()
    {
        PlayerSaveStorage.PlayerSave newData = PlayerSaveLoadProvider.Instance.GetCurrentSave();
        newData.passedDialogueNumbers.Clear();

        PlayerSaveLoadProvider.Instance.SaveInTemp(newData);
    }

    private IEnumerator CheckDeathEnding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (UIManager.Instance.GetActiveWindow() == null)
            {
                if (GetPoints(StoryPointsType.DeathPoints) >= 3)
                {
                    LoadEndScene();
                }

                yield break;
            }
        }
    }
    private void LoadEndScene()
    {
        PackedSceneData endSceneData = new();
        endSceneData.sceneToLoad = SceneNameData.EndScene;

        FadeScreenManager.Instance.FadeScreenIntoNewScene(endSceneData);
    }
}

public enum StoryPointsType
{
    BadPoints,
    GoodPoints,
    DeathPoints
}