using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Talking : MonoBehaviour
{
    public int dialogueNumber;
    int dialogueLineIndex = 0;
    bool isAbleToTalk, isDialogueStarted, isDialogueFinished;
    [HideInInspector] public bool IsDialogueFinished => isDialogueFinished;
    [HideInInspector] public bool isLastDialogueStarted;
    string[] currentDialogue = new string[] {};
    
    [SerializeField] PlayerPoints playerPoints;
    Player player;
    Canvas GUI;
    Transform UIWindow, dialogueWindow, dialogueTextField;

    void Awake()
    {
        GUI = GameObject.Find("GUI").GetComponent<Canvas>();
        UIWindow = GUI.transform.GetChild(0);
        dialogueWindow = UIWindow.transform.GetChild(0);
        dialogueTextField = dialogueWindow.GetChild(0);

        player = GameObject.FindObjectOfType<Player>();
    }
    void Start() => currentDialogue = DialogueInitialisation.dialogues[dialogueNumber];
    void Update()
    {
        if (isAbleToTalk && gameObject.GetComponent<StartDialogue>() == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
                if (currentDialogue[0] != null)
                {
                    if (isDialogueStarted)
                        ChangeDialogue(dialogueNumber);
                    else
                        PlayDialogue(dialogueNumber);
                }
        }
    }

    void OnTriggerStay2D() => isAbleToTalk = true;
    void OnTriggerExit2D() => isAbleToTalk = false;

    void ChangeDialogue(int dialogueNumber)
    {    
        if (dialogueLineIndex+1 < currentDialogue.Length)
        {
            dialogueLineIndex++;  
            dialogueTextField.GetComponent<Text>().text = currentDialogue[dialogueLineIndex];  
        }    
        else if (dialogueLineIndex+1 == currentDialogue.Length)
        {
            player.isAbleToMove = true;
            isDialogueFinished = true;

            NullifyTextField();
            
            if (dialogueNumber == 13 && !isLastDialogueStarted)
                PlayLastDialogue();

            if (gameObject.GetComponent<ChoiceMaking>() != null)
            {
                if (dialogueNumber == 10)
                    PlayKnifeDialogue();
                else
                    gameObject.GetComponent<ChoiceMaking>().enabled = true;
            }
            StartCoroutine(CheckForTextWindow());
        }
    }
    void NullifyTextField()
    {
        dialogueLineIndex = 0;
        dialogueTextField.GetComponent<Text>().text = "";
        Array.Clear(currentDialogue, 0, currentDialogue.Length);
    }
    void PlayLastDialogue()
    {
        isLastDialogueStarted = true;

        if (playerPoints.goodPoints == 2)
        {
            PlayDialogue(16);
        }
        else if (playerPoints.badPoints == 2)
        {
            PlayDialogue(17);
        }
        else // no ending
        {
            PlayDialogue(17);
            playerPoints.badPoints += 1;
        }
    }
    void PlayKnifeDialogue()
    {
        isDialogueStarted = true;

        if (isDialogueStarted)
        {
            if (playerPoints.goodPoints >= 1 && playerPoints.deathPoints == 1)
            {
                gameObject.GetComponent<ChoiceMaking>().enabled = true;
                isDialogueFinished = false;
            }
            else if (!isDialogueStarted)
                PlayDialogue(12);
        }
    }

    // force script to start talking, so it's not dependent on collision
    public void PlayDialogue(int @dialogueNumber)
    {
        ChangePlayerPoints(@dialogueNumber);

        currentDialogue = DialogueInitialisation.dialogues[@dialogueNumber];
        
        dialogueWindow.gameObject.SetActive(true);
        dialogueTextField.GetComponent<Text>().text = currentDialogue[dialogueLineIndex];
        
        player.isAbleToMove = false;
        isAbleToTalk = true;
        isDialogueStarted = true;
    }
    void ChangePlayerPoints(int @dialogueNumber)
    {
        switch (@dialogueNumber)
        {
            case 3:
                playerPoints.goodPoints += 1;
                playerPoints.deathPoints += 1;
                break;
            case 7:
                playerPoints.badPoints += 1;
                break;
            case 8:
                playerPoints.goodPoints += 1;
                playerPoints.deathPoints += 1;
                break;
            case 11:
                playerPoints.deathPoints += 2;
                break;
            case 14:
                playerPoints.goodPoints += 1;
                break;
            case 15:
                playerPoints.badPoints += 1;
                break;
        }
    }
    public void PlayStartDialogue()
    {
        if (currentDialogue[0] != null)
        {
            if (isDialogueStarted)
                ChangeDialogue(dialogueNumber);
            else
                PlayDialogue(dialogueNumber);
        }
    }
    IEnumerator CheckForTextWindow()
    {
        yield return new WaitForEndOfFrame();

        if (dialogueWindow.gameObject.activeSelf == true)
        {
            var choiceLine = dialogueTextField.GetChild(0).gameObject;
            // if no text and no choice to make
            if (dialogueTextField.GetComponent<Text>().text == "" && !choiceLine.activeSelf)
            {
                foreach(Transform child in GUI.transform)
                {
                    if (child != UIWindow)
                        child.gameObject.SetActive(false);
                }
                foreach(Transform child in UIWindow.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        yield break;
    }
}
