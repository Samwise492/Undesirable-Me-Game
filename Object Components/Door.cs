using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action OnStageChanged;
    public event Action OnTeleport;

    public Transform TeleportPosition => teleportPosition;
    public SoundManager.SoundType TransitionSound => transitionSound;
    public bool IsPlayerNear => isPlayerNear;
    public bool IsLoadNewDay => isLoadNewDay;

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
                    SwitchStage();
                }
                else if (Input.GetKeyUp(InputData.interactionKey))
                {
                    SwitchStage();
                }
            }
        }
    }

    private void OnTriggerStay2D() => isPlayerNear = true;
    private void OnTriggerExit2D() => isPlayerNear = false;

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