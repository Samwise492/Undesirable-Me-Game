using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Talking : MonoBehaviour
{
    [SerializeField] int dialogueNumber;
    int dialogueLineIndex = 0;
    bool isAbleToTalk, isDialogueStarted, isDialogueFinished;
    public bool IsDialogueFinished => isDialogueFinished;
    bool isLastDialogueStarted = true;
    string[] currentDialogue = new string[] {};
    
    [SerializeField] PlayerPoints playerPoints;
    Player player;
    Canvas GUI;
    List<GameObject> GUIChilds= new List<GameObject>();
    List<bool> GUIChildsActivity = new List<bool>();
    
    void Awake()
    {
        GUI = GameObject.Find("GUI").GetComponent<Canvas>();
        player = GameObject.FindObjectOfType<Player>();
    }
    void Start()
    {
        currentDialogue = DialogueInitialisation.dialogues[dialogueNumber];

        if (dialogueNumber == 1)
        {
            PlayDialogue(1);
            player.isAbleToMove = false;
        }

        foreach (Transform child in GUI.transform)
        {
            GUIChilds.Add(child.gameObject);
        }
    }
    void OnTriggerStay2D()
    {
        isAbleToTalk = true;
    }
    void OnTriggerExit2D()
    {
        isAbleToTalk = false;
    }
    void Update()
    {
        if (dialogueNumber == 1)
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
        else if (isAbleToTalk)
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
    void ChangeDialogue(int dialogueNumber)
    {    
        if (dialogueLineIndex+1 < currentDialogue.Length)
        {
            dialogueLineIndex++;  
            GUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = currentDialogue[dialogueLineIndex]; // dialogue window    
        }    
        else if (dialogueLineIndex+1 == currentDialogue.Length)
        {
            player.isAbleToMove = true;
            isDialogueFinished = true;
            //GUI.transform.GetChild(0).gameObject.SetActive(false);
            dialogueLineIndex = 0;
            GUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "";
            Array.Clear(currentDialogue, 0, currentDialogue.Length);
            
            if (dialogueNumber == 13 && isLastDialogueStarted)
            {
                isLastDialogueStarted = false;
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
            if (gameObject.GetComponent<ChoiceMaking>() != null)
            {
                if (dialogueNumber == 10)
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
                #region if player is able to choose an ending
                // else if (dialogueNumber == 13)
                // {
                //     if (playerPoints.goodPoints == 2)
                //     {
                //         PlayDialogue(16);
                //     }
                //     else if (playerPoints.badPoints == 2)
                //     {
                //         PlayDialogue(17);
                //     }
                //     else
                //         gameObject.GetComponent<ChoiceMaking>().enabled = true;
                // }
                #endregion
                else
                    gameObject.GetComponent<ChoiceMaking>().enabled = true;
            }
            StartCoroutine(CheckForTextWindow());
                
        }
    }

    public void PlayDialogue(int @dialogueNumber) // force script to start talking; so it's no dependent on collider
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

        currentDialogue = DialogueInitialisation.dialogues[@dialogueNumber];
        
        GUI.gameObject.transform.GetChild(0).gameObject.SetActive(true); // open dialogue window
        GUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = currentDialogue[dialogueLineIndex];
        
        player.isAbleToMove = false;
        isAbleToTalk = true;
        isDialogueStarted = true;
    }
    IEnumerator CheckForTextWindow()
    {
        yield return new WaitForEndOfFrame();

        if (GUI.transform.GetChild(0).gameObject.activeSelf == true)
            if (GUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text == "" && 
            !GUI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.activeSelf)
            {
                GUI.transform.GetChild(0).gameObject.SetActive(false);
            }
            
        yield break;
    }
}
