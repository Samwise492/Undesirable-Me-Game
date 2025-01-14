using System;
using UnityEngine;

public class AnimatorHelper : MonoBehaviour
{
	public event Action OnAnimationEnded;
	public event Action<string> OnNamedAnimationEnded;

	public void NotifyEnd()
	{
		OnAnimationEnded?.Invoke();
    }

	public void NotifyNamedEnd(string animationName)
	{
		OnNamedAnimationEnded?.Invoke(animationName);
    }
}
