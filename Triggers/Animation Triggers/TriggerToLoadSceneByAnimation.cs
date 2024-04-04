using UnityEngine;

public class TriggerToLoadSceneByAnimation : AnimationTrigger
{
    [Header("Manipulated object")]
    [SerializeField]
    private Door door;

    [Header("Arguments")]
    [SerializeField]
    private PackedSceneData sceneToLoadData;

    [Header("System")]
    [SerializeField]
    private bool isFadeBeforeLoading;
    [SerializeField]
    private FadeScreenController fadeScreenController;

    protected override void Start()
    {
        if (isFadeBeforeLoading)
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
        fadeScreenController.FadeScreen(sceneToLoadData);
    }

    private void LoadScene(string eventArg)
    {
        if (door)
        {
            door.SwitchStage();
        }
        else if (sceneToLoadData.sceneToLoadName != "")
        {
            LoadingManager.Instance.LoadScene(sceneToLoadData);
        }
    }

    public override void TriggerAction() { }
}