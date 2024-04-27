using UnityEngine;

public class AnimatorSetter : MonoBehaviour
{
	[SerializeField]
	private Animator animator;

	[Header("Arguments")]
	[SerializeField]
	private string triggerName;
	
	[Space]
	[SerializeField]
	private string boolName;
	[SerializeField]
	private bool boolState;

    private void Start()
    {
        if (triggerName != "")
		{
			animator.SetTrigger(triggerName);
		}

		if (boolName != "")
		{
			animator.SetBool(boolName, boolState);
		}
    }
}
