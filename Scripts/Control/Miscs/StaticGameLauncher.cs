#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

public static class StaticGameLauncher
{
    private static readonly string mainMenuScenePath = "Assets/Undesirable Me/Scenes/Main/Main Menu.unity";

    [MenuItem("Undesirable Me/Start Game")]
    public static void StartGame()
    {
        EditorSceneManager.OpenScene(mainMenuScenePath);
        EditorApplication.EnterPlaymode();
    }
}
#endif