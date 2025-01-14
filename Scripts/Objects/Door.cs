using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action OnStageChange;
    public event Action OnTeleport;

    public Transform TeleportPosition => teleportPosition;
    public bool IsPlayerNear => isPlayerNear;
    public bool IsLoadNewDay => isLoadNewDay;
    public bool IsFadingRequired => isFadingRequired;

    [HideInInspector]
    public bool isAvailable;

    [SerializeField]
    private DoorType type;

    [Space]
    [SerializeField]
    private PackedSceneData sceneToLoadData;

    [Space]
    [SerializeField]
    private GameObject stageToOff;
    [SerializeField]
    private GameObject stageToOn;
    [SerializeField]
    private Transform teleportPosition;
    [SerializeField]
    private SoundManager.SoundType transitionSound;

    [Space]
    [SerializeField]
    private bool isFadingRequired;

    private bool isPlayerNear;
    private bool isLoadNewDay;

    private void Start()
    {
        isAvailable = true;
    }

    private void Update()
    {
        if (isPlayerNear && isAvailable)
        {
            if (UIManager.Instance.GetActiveWindow() == null)
            {
                if (type == DoorType.Teleport)
                {
                    if (!isFadingRequired)
                    {
                        SwitchStage();
                    }
                    else
                    {
                        SwitchStageWithFade();
                    }
                }
                else if (Input.GetKeyUp(InputData.interactionKey))
                {
                    if (!isFadingRequired)
                    {
                        SwitchStage();
                    }
                    else
                    {
                        SwitchStageWithFade();
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D() => isPlayerNear = true;
    private void OnTriggerExit2D() => isPlayerNear = false;

    public void SwitchStage()
    {
        OnStageChange?.Invoke();

        CurseManager.Instance.RandomiseCurse();

        SoundManager.Instance.PlaySound(transitionSound);

        if (sceneToLoadData.sceneToLoad != "")
        {
            isLoadNewDay = true;
            enabled = false;

            LoadingManager.Instance.LoadScene(sceneToLoadData, false);

            return;
        }
        else
        {
            stageToOff.SetActive(false);
            stageToOn.SetActive(true);

            OnTeleport?.Invoke();
        }
    }

    public void SwitchStageWithFade()
    {
        FadeScreenManager.Instance.OnFading -= SwitchStage;
        FadeScreenManager.Instance.OnFading += SwitchStage;

        FadeScreenManager.Instance.FadeScreenInAndOut();
    }
}

public enum DoorType
{
    Normal,
    Teleport,
    SceneLoader
}