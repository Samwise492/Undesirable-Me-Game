using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageData : MonoBehaviour
{
    public int DayNumber => dayNumber;
    public List<Stage> Stages => stages;

    [SerializeField]
    private bool isAutoNaming;
    
    [SerializeField]
    private int dayNumber;
    [SerializeField]
    private List<Stage> stages;

    [Space]
    [SerializeField]
    private List<GameObject> otherObjects;

    [Space]
    [SerializeField]
    private Vector3 playerStartPosition;
    [SerializeField]
    private bool isTurnPlayerOnStart;

    private void OnEnable()
    {
        if (PlayerSaveLoadProvider.Instance.GetCurrentSave().stageDay != dayNumber)
        {
            DisableAllStages();
        }

        if (PlayerSaveLoadProvider.Instance.GetCurrentSave().stageDay == dayNumber)
        {
            ActivateStage(stages.Where(x => x.name == PlayerSaveLoadProvider.Instance.GetCurrentSave().stage).First());

            Player player = FindObjectOfType<Player>();

            player.transform.localPosition = playerStartPosition;
            player.GetComponent<SpriteRenderer>().flipX = isTurnPlayerOnStart;

            GameState.Instance.DelayedTransitTo(CurrentGameState.Default);
        }
    }
    private void OnValidate()
    {
        if (isAutoNaming)
        {
            foreach (Stage stage in stages)
            {
                stage.name = stage.objectLink.name;
            }
        }
    }

    public bool IsActive()
    {
        return stages.Any(x => x.objectLink.activeInHierarchy);
    }

    private void ActivateStage(Stage stageToOn)
    {
        foreach (Stage stage in stages)
        {
            stage.objectLink.SetActive(false);
        }

        stages.Where(x => x == stageToOn).First().objectLink.SetActive(true);
    }

    private void DisableAllStages()
    {
        foreach (Stage stage in stages)
        {
            stage.objectLink.SetActive(false);
        }

        SetOtherObjects(false);
    }

    private void SetOtherObjects(bool state)
    {
        if (otherObjects.Count == 0)
        {
            return;
        }

        foreach (GameObject obj in otherObjects)
        {
            obj.SetActive(state);
        }
    }

    [Serializable]
    public class Stage
    {
        public string name;
        public GameObject objectLink;
    }
}
