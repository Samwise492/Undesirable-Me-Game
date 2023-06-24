using UnityEngine;

public class StampTable : MonoBehaviour
{
    [SerializeField] private PlayerPoints playerPoints;
    [SerializeField] private SpecificDoorBehaviour door;

    private Player player => FindObjectOfType<Player>();
    private GameManager gameManager => FindObjectOfType<GameManager>();
    private SoundManager soundHandler => FindObjectOfType<SoundManager>();

    private Dialogue dialogue => GetComponent<Dialogue>();

    private bool isSealStamped, isAbleToStamp;

    private void Start() 
    {
        dialogue.enabled = false;
        player.AllowMovement();

        gameManager.RealStamp.onClick.AddListener(StampRealStamp);
        gameManager.FakeStamp.onClick.AddListener(StampFakeStamp);
    }
    private void OnDestroy()
    {
        if (gameManager)
        {
            gameManager.RealStamp.onClick.RemoveAllListeners();
            gameManager.FakeStamp.onClick.RemoveAllListeners();
        }
    }

    private void Update()
    {
        if (isAbleToStamp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isSealStamped == false)
                {
                    gameManager.SetWindow(GameManager.UIWindows.StampWindow);
                    player.ProhibitMovement();

                    gameManager.SetCursorState(true);
                }
            }
        }
    }
    
    private void OnTriggerStay2D() => isAbleToStamp = true;
    private void OnTriggerExit2D() => isAbleToStamp = false;

    private void StampRealStamp()
    {
        Stamp();
        playerPoints.AddPoints(PlayerPoints.PointsType.BadPoints, 1);
        
    }
    private void StampFakeStamp()
    {
        Stamp();
        playerPoints.AddPoints(PlayerPoints.PointsType.GoodPoints, 1);
    }
    private void Stamp()
    {
        isSealStamped = true;

        gameManager.SetWindow(null);
        gameManager.SetCursorState(false);
        
        soundHandler.PlaySound(SoundManager.SoundType.Stamp);

        dialogue.enabled = true;
        door.isActivateDialogue = false;
        door.isLocked = false;
    }
}