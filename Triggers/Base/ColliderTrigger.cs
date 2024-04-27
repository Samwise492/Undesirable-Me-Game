using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ColliderTrigger : BaseTrigger
{
    private bool isActionMade;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(InputData.interactionKey) && !isActionMade)
        {
            isActionMade = true;

            CheckTriggerBehaviour();
        }
    }
}
