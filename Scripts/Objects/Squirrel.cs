using UnityEngine;

public class Squirrel : MonoBehaviour
{
	[SerializeField]
	private string[] triggerNames;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private AnimatorHelper animatorHelper;

    private void Start()
    {
		SetRandomAnimation();

        animatorHelper.OnAnimationEnded += SetRandomAnimation;
    }
    private void OnDestroy()
    {
		animatorHelper.OnAnimationEnded -= SetRandomAnimation;
    }

    private void SetRandomAnimation()
	{
		if (triggerNames.Length == 0)
		{
			return;
		}

		int randomIndex = Random.Range(0, triggerNames.Length);

		animator.SetTrigger(triggerNames[randomIndex]);
	}
}
