using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject suicideEndingStage, badEndingStage, goodEndingStage;

    [Space]
    [SerializeField] private PlayerPoints playerPoints;
    [SerializeField] private string mainMenuName;

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
                playerPoints.ResetPoints();
                SceneManager.LoadSceneAsync(mainMenuName, LoadSceneMode.Single);
            }
        }
    }

    private IEnumerator OpenScene()
    {
        if (playerPoints.deathPoints >= 2)
        {
            yield return new WaitForSeconds(1);

            suicideEndingStage.SetActive(true);
        }
        else if (playerPoints.badPoints >= 2)
        {
            yield return new WaitForSeconds(1);

            badEndingStage.SetActive(true);
        }
        else if (playerPoints.goodPoints >= 2)
        {
            yield return new WaitForSeconds(1);

            goodEndingStage.SetActive(true);
        }
    }
}