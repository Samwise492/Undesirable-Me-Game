using UnityEngine;

public abstract class AnimationTrigger : MonoBehaviour, ITrigger
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

    public abstract void TriggerAction();

    private void CheckAnimation(string completedAnimationName)
    {
        TriggerAction();
    }
}