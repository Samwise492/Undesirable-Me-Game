using UnityEngine;

public class FadeScreenControllerToBaseTriggersAdapter : MonoBehaviour
{
	private FadeScreenController fadeScreenController => FindObjectOfType<FadeScreenController>();

	private BaseTrigger[] baseTriggers => FindObjectsOfType<BaseTrigger>(true); 

    private void OnEnable()
    {
        foreach (BaseTrigger baseTrigger in baseTriggers)
        {
            baseTrigger.OnRequestFade += HandleFadeBehaviour;
        }
    }

    private void OnDisable()
    {
        foreach (BaseTrigger baseTrigger in baseTriggers)
        {
            baseTrigger.OnRequestFade -= HandleFadeBehaviour;
        }
    }

    private void HandleFadeBehaviour(BaseTrigger trigger)
    {
        fadeScreenController.FadeScreenInAndOut();

        if (trigger.IsActionOnEnd)
        {
            fadeScreenController.OnEndFading += trigger.TriggerAction;
        }
        else if (trigger.IsActionOnProcess)
        {
            fadeScreenController.OnFading += trigger.TriggerAction;
        }
    }
}
