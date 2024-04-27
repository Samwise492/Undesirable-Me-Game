using UnityEngine;

public class LoadingManagerToFadeScreenControllerAdapter : MonoBehaviour
{
    private LoadingManager loadingManager => FindObjectOfType<LoadingManager>();

	private FadeScreenController fadeScreenController => FindObjectOfType<FadeScreenController>();

    private void OnEnable()
    {
        fadeScreenController.OnPreparedForNewScene += LoadScene;
    }

    private void OnDisable()
    {
        if (fadeScreenController != null)
        {
            fadeScreenController.OnPreparedForNewScene -= LoadScene;
        }
    }

    private void LoadScene(PackedSceneData data)
    {
        loadingManager.LoadScene(data);
    }
}
