using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScenePresenter : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup suicideEndingStage;
    [SerializeField]
    private CanvasGroup deathEndingStage;
    [SerializeField]
    private CanvasGroup badEndingStage;
    [SerializeField]
    private CanvasGroup goodEndingStage;

    private void Start()
    {
        TurnOffAll();

        StartCoroutine(OpenScene());
    }

    private void Update()
    {
        if (suicideEndingStage.alpha == 1 || badEndingStage.alpha == 1 || goodEndingStage.alpha == 1 || deathEndingStage.alpha == 1)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                PlayerProgressProvider.Instance.ResetPoints();
                SceneManager.LoadSceneAsync(SceneNameData.MainMenu, LoadSceneMode.Single);
            }
        }
    }

    private IEnumerator OpenScene()
    {
        if (PlayerProgressProvider.Instance.GetPoints(StoryPointsType.DeathPoints) >= 3)
        {
            yield return new WaitForSeconds(1);

            deathEndingStage.alpha = 1;
            deathEndingStage.blocksRaycasts = true;
        }
        else if (PlayerProgressProvider.Instance.GetPoints(StoryPointsType.BadPoints) >= 2)
        {
            yield return new WaitForSeconds(1);

            badEndingStage.alpha = 1;
            badEndingStage.blocksRaycasts = true;
        }
        else if (PlayerProgressProvider.Instance.GetPoints(StoryPointsType.GoodPoints) >= 2)
        {
            yield return new WaitForSeconds(1);

            goodEndingStage.alpha = 1;
            goodEndingStage.blocksRaycasts = true;
        }
    }

    private void TurnOffAll()
    {
        deathEndingStage.alpha = 0;
        deathEndingStage.blocksRaycasts = false;
        badEndingStage.alpha = 0;
        badEndingStage.blocksRaycasts = false;
        goodEndingStage.alpha = 0;
        goodEndingStage.blocksRaycasts = false;
    }
}