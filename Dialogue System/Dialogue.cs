using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnPlayDialogue;
    [HideInInspector] public UnityEvent OnDialogueStarted, OnDialogueFinished;

    [SerializeField] private bool isAutoPlayDialogue;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private PlayerPoints playerPoints;

    private string[] currentDialogue = new string[] { };

    private int dialogueLineIndex = 0;
    private bool isAbleToTalk, isDialogueGoing, isDialogueFinished, isRepeating;

    private GameManager gameManager => FindObjectOfType<GameManager>();
    private PlayerPointsManager playerPointsManager => FindObjectOfType<PlayerPointsManager>();
    private Player player => FindObjectOfType<Player>();
    private ChoiceMaker choiceMaking => GetComponent<ChoiceMaker>();

    private void Awake()
    {
        if (dialogueData != null)
        {
            SetDialogue(dialogueData);
        }
    }
    private void Start()
    {
        OnPlayDialogue.AddListener(PlayDialogue);

        if (isAutoPlayDialogue)
            PlayDialogue();
    }
    private void OnDestroy()
    {
        OnPlayDialogue.RemoveAllListeners();
    }

    private void Update()
    {
        if (isAbleToTalk)
        {
            if (Input.GetKeyDown(KeyCode.E) && gameManager.GetActiveWindow() != GameManager.UIWindows.ChoiceWindow)
            {
                if (currentDialogue[0] != null)
                {
                    if (isDialogueGoing)
                    {
                        ChangeDialogueLine();
                    }
                    else
                    {
                        OnPlayDialogue?.Invoke();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D()
    {
        if (!isDialogueFinished) 
            isAbleToTalk = true;
    }
    private void OnTriggerExit2D()
    {
        if (!isDialogueFinished)
            isAbleToTalk = false;
    }

    private void SetDialogue(DialogueData dataToSet)
    {
        dialogueData = dataToSet;
        dialogueData.InitialiseDialogue();

        currentDialogue = dataToSet.dialogue;
    }
    public void LoadNewDialogueData(DialogueData newDialogueData, bool isRepeating)
    {
        this.isRepeating = isRepeating;

        if (isRepeating && newDialogueData)
        {
            SetDialogue(newDialogueData);
            EndDialogue();
            gameManager.SetWindow(null);

            isAbleToTalk = true;
            isDialogueFinished = false;
        }
        else if (isRepeating && newDialogueData == null)
        {
            SetDialogue(dialogueData);
            EndDialogue();
            gameManager.SetWindow(null);

            isAbleToTalk = true;
            isDialogueFinished = false;
        }
        else if (newDialogueData == null)
        {
            return;
        }
        else
        {
            SetDialogue(newDialogueData);
            PlayDialogue();
        }
    }

    private void CheckAdditionalData()
    {
        if (choiceMaking != null)
        {
            if (dialogueData.additionalData == "Knife dialogue")
            {
                if (playerPoints.goodPoints >= 1 && playerPoints.deathPoints == 1)
                {
                    choiceMaking.enabled = true;
                }
                else
                {
                    OnPlayDialogue?.Invoke();
                }
            }
        }
    }

    private void PlayDialogue()
    {
        OnDialogueStarted?.Invoke();

        if (dialogueData.additionalData != "")
            playerPointsManager.ChangePlayerPoints(dialogueData.additionalData);
        else
            playerPointsManager.ChangePlayerPoints(dialogueData.name);

        isAbleToTalk = true;
        isDialogueGoing = true;
        player.ProhibitMovement();

        gameManager.SetWindow(GameManager.UIWindows.DialogueWindow);
        gameManager.SetDialogueWindow(dialogueData, dialogueLineIndex);
    }
    private void ChangeDialogueLine()
    {
        if (dialogueLineIndex + 1 == currentDialogue.Length)
        {
            CheckAdditionalData();
            gameManager.SetWindow(null);

            if ((choiceMaking == null || choiceMaking.IsChoiceMade) && !isRepeating)
            {
                EndDialogue();
            }
            else if (choiceMaking.IsChoiceMade == false || isRepeating)
            {
                NullifyText();
                isDialogueGoing = false;

                OnDialogueFinished?.Invoke();
            }
        }
        else
        {
            dialogueLineIndex++;
            gameManager.SetDialogueWindow(dialogueData, dialogueLineIndex);
        }
    }
    private void EndDialogue()
    {
        player.AllowMovement();
        NullifyText();

        isDialogueFinished = true;
        isDialogueGoing = false;
        isAbleToTalk = false;

        OnDialogueFinished?.Invoke();
    }
    private void NullifyText()
    {
        dialogueLineIndex = 0;

        gameManager.ClearDialogueTextField();
    }
}