using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPointsManager : MonoBehaviour
{
    [HideInInspector] public bool isEndingTime;

    [SerializeField] private PlayerPoints playerPoints;
    [SerializeField] private string endSceneName;
    
    [Header("Fading")]
    [SerializeField] private float fadeSpeed;
    [SerializeField] private Image fadingScreen;

    private Player player => FindObjectOfType<Player>();
    private GameManager gameManager => GetComponent<GameManager>();

    private void Start()
    {      
        playerPoints.OnPointsAdded.AddListener(() => StartCoroutine(PlayEnding()));
    }
    private void OnDestroy()
    {
        playerPoints.OnPointsAdded.RemoveAllListeners();
    }

    public void ChangePlayerPoints(string additionalData)
    {
        switch (additionalData)
        {
            case "McKinsey_1_1":
                playerPoints.AddPoints(PlayerPoints.PointsType.GoodPoints, 1);
                playerPoints.AddPoints(PlayerPoints.PointsType.DeathPoints, 1);
                break;
            case "Aubrey_2_1":
                playerPoints.AddPoints(PlayerPoints.PointsType.BadPoints, 1);
                break;
            case "Aubrey_2_2":
                playerPoints.AddPoints(PlayerPoints.PointsType.GoodPoints, 1);
                playerPoints.AddPoints(PlayerPoints.PointsType.DeathPoints, 1);
                break;
            case "Suicide":
                playerPoints.AddPoints(PlayerPoints.PointsType.DeathPoints, 2);
                break;
            case "Doctor_3_1":
                playerPoints.AddPoints(PlayerPoints.PointsType.GoodPoints, 1);
                break;
            case "Doctor_3_2":
                playerPoints.AddPoints(PlayerPoints.PointsType.BadPoints, 1);
                break;
        }
    }

    private IEnumerator PlayEnding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (gameManager.GetActiveWindow() == null)
            {
                if (playerPoints.deathPoints >= 2)
                {
                    fadingScreen.gameObject.SetActive(true);
                    StartCoroutine(FadeScreen());
                }
                else if (playerPoints.badPoints >= 2 || playerPoints.goodPoints >= 2)
                {
                    if (isEndingTime)
                    {
                        fadingScreen.gameObject.SetActive(true);
                        StartCoroutine(FadeScreen());
                    }
                }

                yield break;
            }
        }
    }

    private IEnumerator FadeScreen()
    {
        yield return new WaitForSeconds(1);

        Color varToDecrease = fadingScreen.color;
        player.ProhibitMovement();

        while (true)
        { 
            varToDecrease.a += fadeSpeed;
            fadingScreen.color = varToDecrease;
            yield return new WaitForSeconds(0.15f);
        
            if (fadingScreen.color.a >= 1)
            {
                SceneManager.LoadSceneAsync(endSceneName, LoadSceneMode.Single);
                fadingScreen.gameObject.SetActive(false);
                
                yield break;
            }
        }
    }  
}