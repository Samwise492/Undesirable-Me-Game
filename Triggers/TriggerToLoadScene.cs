using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerToLoadScene : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField]
    private Dialogue dialogueTrigger;
    [Tooltip("If dialogueTrigger's finished DialogueData == this DialogueData, the scene will be loaded")]
    [SerializeField]
    private List<DialogueData> dataToCheck;

    [Space]
    [SerializeField]
    private AnimationEventDispatcher animationTrigger;

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

    private void Start()
    {
        HandleSubscriptions();

        if (door)
        {
            door.enabled = false; // we must not have it available, otherwise the player can go through the door w/o trigger.
        }
    }
    private void OnDestroy()
    {
        UnsubscribeAll();
    }

    private void HandleSubscriptions()
    {
        if (dialogueTrigger)
        {
            if (isFadeBeforeLoading)
            {
                dialogueTrigger.OnDialogueFinished += FadeScreen;
            }
            else
            {
                dialogueTrigger.OnDialogueFinished += LoadScene;
            }
        }

        if (animationTrigger)
        {
            if (isFadeBeforeLoading)
            {
                animationTrigger.OnAnimationCompleted.AddListener(FadeScreen);
            }
            else
            {
                animationTrigger.OnAnimationCompleted.AddListener(LoadSceneAfterAnimation);
            }
        }
    }
    private void UnsubscribeAll()
    {
        if (dialogueTrigger)
        {
            dialogueTrigger.OnDialogueFinished -= FadeScreen;
            dialogueTrigger.OnDialogueFinished -= LoadScene;
        }

        if (animationTrigger)
        {
            animationTrigger.OnAnimationCompleted.RemoveListener(FadeScreen);
            animationTrigger.OnAnimationCompleted.RemoveListener(LoadSceneAfterAnimation);
        }
    }

    private void FadeScreen(DialogueData eventArg)
    {
        if (dataToCheck.Contains(eventArg))
        {
            FadeScreen();
        }
    }
    private void FadeScreen(string eventArg)
    {
        FadeScreen();
    }
    private void FadeScreen()
    {
        fadeScreenController.FadeScreen(sceneToLoadData);
    }

    private void LoadSceneAfterAnimation(string eventArg)
    {
        LoadScene();
    }

    private void LoadScene(DialogueData eventArg)
    {
        if (eventArg == dataToCheck.Any())
        {
            LoadScene();
        }
    }
    private void LoadScene()
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
}
