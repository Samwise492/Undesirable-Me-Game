using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampTable : MonoBehaviour
{
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] DoorBehaviour doorToLock;
    GameObject stampTableScreen;
    Talking talkingComponent;
    Player player;
    SoundHandler soundHandler;
    bool isSealStamped, isAbleToStamp;

    void Start() 
    {
        soundHandler = GameObject.FindObjectOfType<SoundHandler>();
        stampTableScreen = GameObject.FindObjectOfType<Canvas>().transform.GetChild(0).gameObject;
        player = GameObject.FindObjectOfType<Player>();
        talkingComponent = gameObject.GetComponent<Talking>();

        talkingComponent.enabled = false;
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
                    stampTableScreen.SetActive(true);
                    player.isAbleToMove = false;
                }
            }
        }
    }
    
    void OnTriggerStay2D() => isAbleToStamp = true;
    void OnTriggerExit2D() => isAbleToStamp = false;

    public void RealSeal()
    {
        talkingComponent.PlayDialogue(talkingComponent.dialogueNumber);
        
        isSealStamped = true;
        doorToLock.isLocked = false;
        playerPoints.badPoints += 1;

        stampTableScreen.SetActive(false);
        soundHandler.stamp.Play();
        talkingComponent.enabled = true;
    }
    public void FakeSeal()
    {
        talkingComponent.PlayDialogue(talkingComponent.dialogueNumber);
        
        isSealStamped = true;
        doorToLock.isLocked = false;
        playerPoints.goodPoints += 1;

        stampTableScreen.SetActive(false);
        soundHandler.stamp.Play();
        talkingComponent.enabled = true;
    }

}
