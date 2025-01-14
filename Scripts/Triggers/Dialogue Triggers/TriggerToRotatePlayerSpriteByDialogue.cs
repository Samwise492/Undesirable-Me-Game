using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToRotatePlayerSpriteByDialogue : DialogueTrigger
{
    private Player player => FindObjectOfType<Player>();

    public override void TriggerAction()
    {
        var spriteComponent = player.GetComponent<SpriteRenderer>();
        
        spriteComponent.flipX = !spriteComponent.flipX;
        
        EndTrigger();
    }
}
