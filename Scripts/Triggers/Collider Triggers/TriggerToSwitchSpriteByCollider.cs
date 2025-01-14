using UnityEngine;

public class TriggerToSwitchSpriteByCollider : ColliderTrigger
{
    [Space]
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private bool isFlipX;
    [SerializeField]
    private bool isFlipXOnDefault;

    public override void TriggerAction()
    {
        if (isFlipX)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX; 
        }
        else if (isFlipXOnDefault)
        {
            spriteRenderer.flipX = false;
        }

        EndTrigger();
    }
}