using UnityEngine;

public abstract class AnimationTrigger : BaseTrigger
{
    [SerializeField]
    internal AnimationEventDispatcher animationTrigger;

    protected virtual void Start()
    {
        animationTrigger.OnAnimationCompleted.AddListener(CheckAnimation);
    }
    protected virtual void OnDestroy()
    {
        animationTrigger.OnAnimationCompleted.RemoveListener(CheckAnimation);
    }

    private void CheckAnimation(string completedAnimationName)
    {
        CheckTriggerBehaviour();
    }
}