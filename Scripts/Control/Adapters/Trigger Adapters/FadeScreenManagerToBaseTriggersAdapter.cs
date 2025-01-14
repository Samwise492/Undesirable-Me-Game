using UnityEngine;

public class FadeScreenManagerToBaseTriggersAdapter : MonoBehaviour
{
	private FadeScreenManager fadeScreenManager => FindObjectOfType<FadeScreenManager>();

	private BaseTrigger[] baseTriggers => FindObjectsOfType<BaseTrigger>(true); 

    private void OnEnable()
    {
        foreach (BaseTrigger baseTrigger in baseTriggers)
        {
            baseTrigger.OnRequestOverrideFadeLength += fadeScreenManager.OverridePauseTime;
            baseTrigger.OnRequestFade += HandleFadeBehaviour;
        }
    }

    private void OnDisable()
    {
        if (fadeScreenManager)
        {
            if (baseTriggers.Length > 0)
            {
                foreach (BaseTrigger baseTrigger in baseTriggers)
                {
                    baseTrigger.OnRequestOverrideFadeLength -= fadeScreenManager.OverridePauseTime;
                    baseTrigger.OnRequestFade -= HandleFadeBehaviour;
                }
            }
        }
    }

    private void HandleFadeBehaviour(BaseTrigger trigger)
    {
        fadeScreenManager.FadeScreenInAndOut();

        if (trigger.IsActionOnEnd)
        {
            fadeScreenManager.OnEndFading += trigger.TriggerAction;
        }
        else if (trigger.IsActionOnProcess)
        {
            fadeScreenManager.OnFading += trigger.TriggerAction;
        }
    }
}
