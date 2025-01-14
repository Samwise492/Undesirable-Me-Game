using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToSetColliderByDialogue : DialogueTrigger
{
    [SerializeField] 
    private GameObject colliderObject;
    [SerializeField]
    private bool stateToSet;
    [SerializeField] 
    private bool isTriggerCollider = true;

    private Collider2D specificCollider;
    
    protected override void Start()
    {
        base.Start();
        
        GetCorrectCollider();

        specificCollider.enabled = !stateToSet;
    }

    private void GetCorrectCollider()
    {
        var colliders = colliderObject.GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            if (isTriggerCollider && collider.isTrigger)
            {
                specificCollider = collider;
            }
            else if (!isTriggerCollider && !collider.isTrigger)
            {
                specificCollider = collider;
            }
        }
    }

    public override void TriggerAction()
    {
        specificCollider.enabled = stateToSet;
    }
}
