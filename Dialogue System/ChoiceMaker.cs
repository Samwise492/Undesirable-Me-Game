using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Dialogue))]
public class ChoiceMaker : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int> OnChoiceMade;

    public bool IsChoiceMade => isChoiceMade;

    [SerializeField] private ChoiceContainer[] choiceContainers;

    private GameManager gameManager => FindObjectOfType<GameManager>();
    private Player player => FindObjectOfType<Player>();
    private Dialogue dialogue => GetComponent<Dialogue>();
    
    private List<Button> choiceButtons = new List<Button>();
    private bool isChoiceMade, isInitialised;

    private void Start()
    {
        dialogue.OnDialogueStarted?.AddListener(delegate { if (!isInitialised) InitialiseButtons(); });
        dialogue.OnDialogueFinished?.AddListener(delegate { if (!isChoiceMade) gameManager.SetWindow(GameManager.UIWindows.ChoiceWindow); });
    }
    private void OnDestroy()
    {
        dialogue.OnDialogueStarted?.RemoveAllListeners();
        dialogue.OnDialogueFinished?.RemoveAllListeners();
    }

    private void Update()
    {
        if (gameManager.GetActiveWindow() == GameManager.UIWindows.ChoiceWindow)
        {
            for (int i = 0; i < choiceButtons.Count; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    choiceButtons[i].onClick.Invoke();
                }
            }
        }
    }
    
    private void InitialiseButtons()
    {
        for (int i = 0; i < gameManager.GetChoiceLines().Length; i++)
        {
            int cachedIndex = i;

            choiceButtons.Add(gameManager.GetChoiceLines()[cachedIndex]);

            choiceButtons[cachedIndex].GetComponentInChildren<Text>().text = choiceContainers[cachedIndex].choiceLine;
            choiceButtons[cachedIndex].onClick.AddListener(() => MakeChoice(choiceContainers[cachedIndex].dialogueData, choiceContainers[cachedIndex].isRepeating, cachedIndex+1));
        }

        isInitialised = true;
    }
    private void DeinitialiseButtons()
    {
        for (int i = 0; i < gameManager.GetChoiceLines().Length; i++)
        {
            int cachedIndex = i;

            choiceButtons[cachedIndex].GetComponentInChildren<Text>().text = "";
            choiceButtons[cachedIndex].onClick.RemoveAllListeners();//RemoveListener(() => MakeChoice(choiceContainers[cachedIndex].dialogueData, choiceContainers[cachedIndex].isRepeating, cachedIndex + 1));
        }
    }

    private void MakeChoice(DialogueData dialogueData, bool isRepeating, int dialogueIndex)
    {
        if (dialogueData)
        {
            gameManager.SetWindow(GameManager.UIWindows.DialogueWindow);
            dialogue.LoadNewDialogueData(dialogueData, isRepeating);
        }
        else
        {
            gameManager.SetWindow(null);
            player.AllowMovement();
        }

        if (!isRepeating)
        {
            isChoiceMade = true;
            dialogue.OnDialogueFinished.RemoveListener(() => gameManager.SetWindow(GameManager.UIWindows.ChoiceWindow));
            DeinitialiseButtons();
        }

        OnChoiceMade?.Invoke(dialogueIndex);
    }
}

[Serializable]
public class ChoiceContainer
{
    public string choiceLine;
    public DialogueData dialogueData;
    public bool isRepeating;
}