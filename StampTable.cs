using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampTable : MonoBehaviour
{
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] Canvas canvas;
    [SerializeField] int dialogueNumber;
    [SerializeField] DoorBehaviour doorToLock;
    bool isSealStamped;
    bool isAbleToStamp;
    Player player;
    SoundHandler soundHandler;
    
    
    void Start() 
    {
        gameObject.GetComponent<Talking>().enabled = false;

        soundHandler = GameObject.FindObjectOfType<SoundHandler>();
        player = GameObject.FindObjectOfType<Player>();
        player.isAbleToMove = true;
    }
    void Update()
    {
        if (isAbleToStamp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isSealStamped == false)
                {
                    canvas.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    player.isAbleToMove = false;
                }
            }
        }
    }

    public void RealSeal()
    {
        gameObject.GetComponent<Talking>().PlayDialogue(dialogueNumber);
        
        isSealStamped = true;
        doorToLock.isLocked = false;
        playerPoints.badPoints += 1;

        canvas.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        soundHandler.stamp.Play();
        gameObject.GetComponent<Talking>().enabled = true;
    }
    public void FakeSeal()
    {
        gameObject.GetComponent<Talking>().PlayDialogue(dialogueNumber);
        
        isSealStamped = true;
        doorToLock.isLocked = false;
        playerPoints.goodPoints += 1;

        canvas.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        soundHandler.stamp.Play();
        gameObject.GetComponent<Talking>().enabled = true;
    }

    void OnTriggerStay2D()
    {
        isAbleToStamp = true;
    }
    void OnTriggerExit2D()
    {
        isAbleToStamp = false;
    }
}
