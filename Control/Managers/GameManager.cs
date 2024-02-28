using UnityEngine;
using UnityEngine.SceneManagement;

// ???
public class GameManager : MonoBehaviour
{
    public static bool IsEndScene { get; private set; }
    public static string EndSceneName { get; private set; }

    [SerializeField]
    private string endSceneName;

    private static bool isEndScene;

    private void Awake()
    {
        isEndScene = SceneManager.GetActiveScene().name == endSceneName;

        EndSceneName = endSceneName;
        IsEndScene = isEndScene;
    }
}
