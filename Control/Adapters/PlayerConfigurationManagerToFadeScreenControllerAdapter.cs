using UnityEngine;

public class PlayerConfigurationManagerToFadeScreenControllerAdapter : MonoBehaviour
{
    private PlayerConfigurationManager playerConfigurationManager => FindObjectOfType<PlayerConfigurationManager>();

	private FadeScreenController fadeScreenController => FindObjectOfType<FadeScreenController>();

    private void OnEnable()
    {
        playerConfigurationManager.OnStartEnding += LoadEndScene;
    }
    private void OnDisable()
    {
        if (playerConfigurationManager && fadeScreenController)
            playerConfigurationManager.OnStartEnding -= LoadEndScene;
    }

    private void LoadEndScene()
    {
        PackedSceneData endSceneData = new();
        endSceneData.sceneToLoadName = GameManager.EndSceneName;

        fadeScreenController.FadeScreenIntoNewScene(endSceneData);
    }
}
