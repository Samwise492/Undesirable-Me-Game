using UnityEngine;

public class LastDialogueHandler : MonoBehaviour
{
    [SerializeField] private Dialogue talkingCharacter;
    [SerializeField] private PlayerPoints playerPoints;

    [Header("Dialogue Data")]
    [SerializeField] private DialogueData badEnding;
    [SerializeField] private DialogueData goodEnding;

    private PlayerPointsManager playerPointsManager => FindObjectOfType<PlayerPointsManager>();

    private void Start()
    {
        talkingCharacter.OnDialogueFinished.AddListener(ChooseLastDialogue);
    }
    private void OnDestroy()
    {
        talkingCharacter.OnDialogueFinished.RemoveAllListeners();
    }

    private void ChooseLastDialogue()
    {
        talkingCharacter.OnDialogueFinished.RemoveListener(ChooseLastDialogue);

        if (playerPoints.goodPoints == 2)
        {
            talkingCharacter.LoadNewDialogueData(goodEnding, false);
        }
        else if (playerPoints.badPoints == 2)
        {
            talkingCharacter.LoadNewDialogueData(badEnding, false);
        }
        else
        {
            playerPoints.AddPoints(PlayerPoints.PointsType.BadPoints, 1);
            talkingCharacter.LoadNewDialogueData(badEnding, false);
        }

        playerPointsManager.isEndingTime = true;
        playerPoints.AddPoints(PlayerPoints.PointsType.DeathPoints, 0);
    }
}