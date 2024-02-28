using UnityEngine;
using UnityEngine.Events;

public class AnimationEventDispatcher : MonoBehaviour
{
    public UnityAnimationEvent OnAnimationStarted;
    public UnityAnimationEvent OnAnimationCompleted;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animation _animation;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (animator)
        {
            for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];

                SubscribeClip(clip);
            }
        }

        if (_animation)
        {
            AnimationClip clip = _animation.clip;

            SubscribeClip(clip);
        }
    }

    public void AnimationStartHandler(string name)
    {
        OnAnimationStarted?.Invoke(name);
    }
    public void AnimationCompleteHandler(string name)
    {
        OnAnimationCompleted?.Invoke(name);
    }

    private void SubscribeClip(AnimationClip clip)
    {
        AnimationEvent animationStartEvent = new()
        {
            time = 0,
            functionName = "AnimationStartHandler",
            stringParameter = clip.name
        };

        AnimationEvent animationEndEvent = new()
        {
            time = clip.length,
            functionName = "AnimationCompleteHandler",
            stringParameter = clip.name
        };

        clip.AddEvent(animationStartEvent);
        clip.AddEvent(animationEndEvent);
    }
}

[System.Serializable]
public class UnityAnimationEvent : UnityEvent<string> { };