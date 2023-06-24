using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnSceneChange;

    [SerializeField] private string sceneToLoadName;
    [SerializeField] private GameObject stageToOff, stageToOn;
    [SerializeField] private Transform teleportPosition;
    [SerializeField] private SoundManager.SoundType transitionSound;
    
    private SoundManager soundHandler => FindObjectOfType<SoundManager>();
    private GameManager gameManager => FindObjectOfType<GameManager>();
    private Player player => FindObjectOfType<Player>();
    
    private SpecificDoorBehaviour specificBehaviour => GetComponent<SpecificDoorBehaviour>();

    private bool isPlayerApproached, isLoadNewDay;

    private void Update()
    {
        if (isPlayerApproached)
        {
            if (specificBehaviour != null)
            {
                HandleDoorBehaviour();
            }
            else if (gameManager.GetActiveWindow() == null) 
            {
                if (Input.GetKeyUp(KeyCode.W)) 
                    SwitchStage();
            }
        }
    }

    private void OnTriggerStay2D() => isPlayerApproached = true;
    private void OnTriggerExit2D() => isPlayerApproached = false;

    public void SwitchStage()
    {
        OnSceneChange?.Invoke();

        if (sceneToLoadName != "")
        {
            isLoadNewDay = true;
            StartCoroutine(LoadNewScene(sceneToLoadName));
            
            return;
        }

        stageToOff.SetActive(false);
        stageToOn.SetActive(true);

        soundHandler.PlaySound(transitionSound);
        TeleportPlayer();   
    }

    private void HandleDoorBehaviour()
    {
        if (specificBehaviour.isLocked)
        {
            if (Input.GetKeyUp(KeyCode.W))
                soundHandler.PlaySound(SoundManager.SoundType.ClosedDoor);
        }
        else if (!specificBehaviour.isLocked)
        {
            if (Input.GetKeyUp(KeyCode.W))
                SwitchStage();
        }
    }
    
    private void TeleportPlayer()
    {
        if (teleportPosition == null && !isLoadNewDay)
            player.transform.position = new Vector3(0f, player.transform.position.y, player.transform.position.z);
        else if (!isLoadNewDay)
            player.transform.position = teleportPosition.position;
    }

    private IEnumerator LoadNewScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync("Loading Screen", LoadSceneMode.Additive);

        yield return new WaitForSeconds(0.2f);

        HandleLoadingScreen(sceneName);
        SceneManager.UnloadSceneAsync(currentScene);

        yield break;
    }
    private void HandleLoadingScreen(string sceneName)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading Screen"));

        foreach(Transform child in GameObject.Find("Days").transform)
        {
            child.gameObject.SetActive(true);
        }

        GameObject newSceneTitle = GameObject.Find(sceneName);

        foreach(Transform child in GameObject.Find("Days").transform)
        {
            if (child.name != newSceneTitle.name)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}