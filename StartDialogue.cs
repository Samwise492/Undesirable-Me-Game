using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Talking))]
public class StartDialogue : MonoBehaviour
{
    Talking talkingComponent;
    Player player;

    void Start()
    {
        talkingComponent = gameObject.GetComponent<Talking>();
        player = FindObjectOfType<Player>();

        talkingComponent.PlayDialogue(1);
        player.isAbleToMove = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            talkingComponent.PlayStartDialogue();
    }
}
