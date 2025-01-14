using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ColliderTrigger : BaseTrigger
{
    private bool isActionMade;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(InputData.interactionKey) && !isActionMade && !PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id))
        {
            isActionMade = true;

            CheckTriggerBehaviour();
        }
    }
}
