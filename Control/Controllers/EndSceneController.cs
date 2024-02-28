using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject suicideEndingStage;
    [SerializeField]
    private GameObject badEndingStage;
    [SerializeField]
    private GameObject goodEndingStage;

    [Space][SerializeField]
    private PlayerConfiguration playerConfiguration;
    [SerializeField] 
    private string mainMenuName;

    private void Start()
    {
        StartCoroutine(OpenScene());
    }
    
    private void Update()
    {
        if (suicideEndingStage.activeInHierarchy || badEndingStage.activeInHierarchy || goodEndingStage.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                playerConfiguration.ResetPoints();
                SceneManager.LoadSceneAsync(mainMenuName, LoadSceneMode.Single);
            }
        }
    }

    private IEnumerator OpenScene()
    {
        if (playerConfiguration.DeathPoints >= 2)
        {
            yield return new WaitForSeconds(1);

            suicideEndingStage.SetActive(true);
        }
        else if (playerConfiguration.BadPoints >= 2)
        {
            yield return new WaitForSeconds(1);

            badEndingStage.SetActive(true);
        }
        else if (playerConfiguration.GoodPoints >= 2)
        {
            yield return new WaitForSeconds(1);

            goodEndingStage.SetActive(true);
        }
    }
}