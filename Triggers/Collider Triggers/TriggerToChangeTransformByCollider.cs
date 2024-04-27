using UnityEngine;

public class TriggerToChangeTransformByCollider : ColliderTrigger
{
    [Space]
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private bool isChangePosition;
    [SerializeField]
    private Vector3 newPosition;

    [SerializeField]
    private Rigidbody2D rbToOff;

    public override void TriggerAction()
    {
        if (rbToOff)
        {
            rbToOff.simulated = false;
        }

        if (isChangePosition)
        {
            targetObject.transform.localPosition = newPosition;

            EndTrigger();
        }
    }
}
