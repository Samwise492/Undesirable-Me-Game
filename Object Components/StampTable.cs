using UnityEngine;

public class StampTable : MonoBehaviour
{
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
        player.AllowMovement();

        UIManager.Instance.RealStamp.onClick.AddListener(StampRealStamp);
        UIManager.Instance.FakeStamp.onClick.AddListener(StampFakeStamp);
    }
    private void OnDestroy()
    {
        if (UIManager.Instance)
        {
            UIManager.Instance.RealStamp.onClick.RemoveAllListeners();
            UIManager.Instance.FakeStamp.onClick.RemoveAllListeners();
        }
    }

    private void Update()
    {
        if (isAbleToStamp)
        {
            if (Input.GetKeyDown(InputData.interactionKey))
            {
                if (isSealStamped == false)
                {
                    UIManager.Instance.SetWindow(UIManager.UIWindows.StampWindow);
                    player.ProhibitMovement();

                    UIManager.Instance.SetCursorState(true);
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

    private void StampRealStamp()
    {
        Stamp();

        PointsToAddAfterDialogue[] pointsData = new PointsToAddAfterDialogue[] { new(PlayerConfiguration.PointsType.BadPoints, 1) };

        playerConfiguration.AddPoints(pointsData);
        playerConfiguration.ChangeKey(new int[] { -1 });

    }
    private void StampFakeStamp()
    {
        Stamp();

        PointsToAddAfterDialogue[] pointsData = new PointsToAddAfterDialogue[] { new(PlayerConfiguration.PointsType.GoodPoints, 1) };

        playerConfiguration.AddPoints(pointsData);
    }
    private void Stamp()
    {
        isSealStamped = true;

        UIManager.Instance.SetWindow(null);
        UIManager.Instance.SetCursorState(false);

        soundHandler.PlaySound(SoundManager.SoundType.Stamp);

        stampTableDialogue.enabled = true;
        door.isActivateDialogue = false;
        door.isLocked = false;
    }
}