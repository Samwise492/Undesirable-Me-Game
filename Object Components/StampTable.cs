using System;
using UnityEngine;

public class StampTable : MonoBehaviour
{
    public event Action OnPlayerInteracted;
    public event Action OnStamped;

    [SerializeField]
    private PlayerConfiguration playerConfiguration;

    [Space]
    [SerializeField]
    private SoundManager soundHandler;

    [Space]
    [SerializeField]
    private SpecificDoorBehaviour door;
    [SerializeField]
    private Player player;

    [Space]
    [SerializeField]
    private Dialogue stampTableDialogue;

    private bool isSealStamped;
    private bool isAbleToStamp;

    private void Start()
    {
        stampTableDialogue.enabled = false;
    }

    private void Update()
    {
        if (isAbleToStamp)
        {
            if (Input.GetKeyDown(InputData.interactionKey))
            {
                if (isSealStamped == false)
                {
                    player.ProhibitMovement();

                    UIManager.Instance.SetCursorState(true);

                    OnPlayerInteracted?.Invoke();
                }
            }
        }
    }

    private void OnTriggerStay2D()
    {
        isAbleToStamp = true;
    }
    private void OnTriggerExit2D()
    {
        isAbleToStamp = false;
    }

    public void StampRealStamp()
    {
        Stamp();

        PointsToAddAfterDialogue[] pointsData = new PointsToAddAfterDialogue[] { new(PlayerConfiguration.PointsType.BadPoints, 1) };

        playerConfiguration.AddPoints(pointsData);
        playerConfiguration.ChangeKey(new int[] { -1 });

    }
    public void StampFakeStamp()
    {
        Stamp();

        PointsToAddAfterDialogue[] pointsData = new PointsToAddAfterDialogue[] { new(PlayerConfiguration.PointsType.GoodPoints, 1) };

        playerConfiguration.AddPoints(pointsData);
    }
    private void Stamp()
    {
        isSealStamped = true;

        UIManager.Instance.SetCursorState(false);

        soundHandler.PlaySound(SoundManager.SoundType.Stamp);

        stampTableDialogue.enabled = true;
        door.isActivateDialogue = false;
        door.isLocked = false;

        OnStamped?.Invoke();
    }
}