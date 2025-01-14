using UnityEngine;

public class TriggerToLoadSceneByAnimation : AnimationTrigger
{
    [Header("Manipulated object")]
    [SerializeField]
    private Door door;

    [Header("Arguments")]
    [SerializeField]
    private PackedSceneData sceneToLoadData;

    protected override void Start()
    {
        if (isFadeBeforeSceneLoad)
        {
            animationTrigger.OnAnimationCompleted.AddListener(FadeScreen);
        }
        else
        {
            animationTrigger.OnAnimationCompleted.AddListener(LoadScene);
        }

        if (door)
        {
            door.enabled = false; // we must not have it available, otherwise the player can go through the door w/o trigger.
        }
    }
    protected override void OnDestroy()
    {
        animationTrigger.OnAnimationCompleted.RemoveListener(FadeScreen);
        animationTrigger.OnAnimationCompleted.RemoveListener(LoadScene);
    }

    private void FadeScreen(string eventArg)
    {
        FadeScreenManager.Instance.FadeScreenIntoNewScene(sceneToLoadData);
    }

    private void LoadScene(string eventArg)
    {
        if (door)
        {
            door.SwitchStage();
        }
        else if (sceneToLoadData.sceneToLoad != "")
        {
            LoadingManager.Instance.LoadScene(sceneToLoadData, false);
        }
    }

    public override void TriggerAction() { }
}