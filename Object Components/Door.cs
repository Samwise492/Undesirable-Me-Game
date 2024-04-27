using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action OnStageChanged;
    public event Action OnTeleport;
    public event Action OnChangeStageWithFade;

    public Transform TeleportPosition => teleportPosition;
    public SoundManager.SoundType TransitionSound => transitionSound;
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
                        OnChangeStageWithFade?.Invoke();
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
                        OnChangeStageWithFade?.Invoke();
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D() => isPlayerNear = true;
    private void OnTriggerExit2D() => isPlayerNear = false;

    public void AskForFading()
    {
        OnChangeStageWithFade?.Invoke();
    }
    public void SwitchStage()
    {
        OnStageChanged?.Invoke();

        if (sceneToLoadData.sceneToLoadName != "")
        {
            isLoadNewDay = true;

            LoadingManager.Instance.LoadScene(sceneToLoadData);

            return;
        }
        else
        {
            stageToOff.SetActive(false);
            stageToOn.SetActive(true);

            OnTeleport?.Invoke();
        }
    }
}

public enum DoorType
{
    Normal,
    Teleport,
    SceneLoader
}