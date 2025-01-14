using System;
using UnityEngine;

public class StampTable : MonoBehaviour
{
    public event Action OnPlayerInteracted;
    public event Action OnStamped;

    [Space]
    [SerializeField]
    private SpecificDoorBehaviour door;
    [SerializeField]
    private Player player;

    [Space]
    [SerializeField]
    private BaseDialogue stampTableDialogue;

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

        PointsToAddAfterDialogue[] pointsData = new PointsToAddAfterDialogue[] { new(StoryPointsType.BadPoints, 1) };

        PlayerProgressProvider.Instance.SetPoints(pointsData);
        PlayerProgressProvider.Instance.SetKeyValue(new int[] { -1 });

    }
    public void StampFakeStamp()
    {
        Stamp();

        PointsToAddAfterDialogue[] pointsData = new PointsToAddAfterDialogue[] { new(StoryPointsType.GoodPoints, 1) };

        PlayerProgressProvider.Instance.SetPoints(pointsData);
    }
    private void Stamp()
    {
        isSealStamped = true;

        SoundManager.Instance.PlaySound(SoundManager.SoundType.Stamp);

        stampTableDialogue.enabled = true;
        door.isActivateDialogue = false;
        door.isLocked = false;

        OnStamped?.Invoke();
    }
}