using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    [SerializeField] Scenes sceneToOff;
    [SerializeField] Scenes sceneToOn;
    [SerializeField] DoorSounds doorSound;
    [SerializeField] Transform teleportPosition;
    bool onTrigger, doesLoadNewDay;
    public bool isSceneChanged;
    Canvas GUI;
    SoundHandler soundHandler;
    SceneHandler_LevelOne sceneHandler_One;
    SceneHandler_LevelTwo sceneHandler_Two;

    void Start()
    {
        GUI = GameObject.Find("GUI").GetComponent<Canvas>();
        soundHandler = GameObject.FindObjectOfType<SoundHandler>();

        sceneHandler_One = GameObject.FindObjectOfType<SceneHandler_LevelOne>();
        sceneHandler_Two = GameObject.FindObjectOfType<SceneHandler_LevelTwo>();
    }
    void Update()
    {
        if (onTrigger == true)
        {
            // if door makes specific behaviour
            if (gameObject.GetComponent<DoorBehaviour>() != null)
            {
                if (gameObject.GetComponent<DoorBehaviour>().isLocked)
                    if (Input.GetKeyUp(KeyCode.W))
                    {
                        // WIP
                    }
                else if (!gameObject.GetComponent<DoorBehaviour>().isLocked)
                    if (Input.GetKeyUp(KeyCode.W))
                    {
                        ChangeScene();
                    }
            }
            // if door does not make specific behaviour
            else if (!GUI.transform.GetChild(0).gameObject.activeSelf) 
                if (Input.GetKeyUp(KeyCode.W))
                {
                    ChangeScene();
                }
        }
    }

    void OnTriggerStay2D(Collider2D col) => onTrigger = true;
    void OnTriggerExit2D(Collider2D col) => onTrigger = false;

    public void ChangeScene()
    {
        isSceneChanged = true;
        
        OffScene();
        OnScene();
        
        MakeSound();

        TeleportPlayer();   
    }

    public void OffScene()
    {
        switch (sceneToOff)
        {
            case Scenes.Office:
                sceneHandler_One.officeScene.SetActive(false);
                break;
            case Scenes.HospitalHall:
                sceneHandler_One.hospitalHallScene.SetActive(false);
                break;
            case Scenes.Archive:
                sceneHandler_One.archiveScene.SetActive(false);
                break;

            case Scenes.Room:
                sceneHandler_Two.roomScene.SetActive(false);
                break;
            case Scenes.HouseHall:
                sceneHandler_Two.houseHallScene.SetActive(false);
                break;
            case Scenes.Kitchen:
                sceneHandler_Two.kitchenScene.SetActive(false);
                break;

            case Scenes.None:
                break;
        }
    }
    public void OnScene()
    {
        switch (sceneToOn)
        {
            case Scenes.Office:
                sceneHandler_One.officeScene.SetActive(true);
                break;
            case Scenes.HospitalHall:
                sceneHandler_One.hospitalHallScene.SetActive(true);
                break;
            case Scenes.Archive:
                sceneHandler_One.archiveScene.SetActive(true);
                break;               
            case Scenes.HospitalExit:
                doesLoadNewDay = true;
                StartCoroutine(LoadNewDay(2));
                break;

            case Scenes.Room:
                sceneHandler_Two.roomScene.SetActive(true);
                break;
            case Scenes.HouseHall:
                sceneHandler_Two.houseHallScene.SetActive(true);
                break;
            case Scenes.Kitchen:
                sceneHandler_Two.kitchenScene.SetActive(true);
                break;

            case Scenes.None:
                break;
        }
    }
    public void MakeSound()
    {
        switch (doorSound)
        {
            case DoorSounds.Door:
                soundHandler.door.Play();
                break;
            case DoorSounds.WayIn:
                soundHandler.wayIn.Play();
                break;
            case DoorSounds.Stairs:
                soundHandler.stairs.Play();
                break;
        }
    }
    public void TeleportPlayer()
    {
        if (teleportPosition == null)
        {
            if (!doesLoadNewDay)
                GameObject.Find("Player").transform.position = new Vector3(0f, GameObject.Find("Player").transform.position.y, GameObject.Find("Player").transform.position.z);
        }
        else 
        {
            GameObject.Find("Player").transform.position = teleportPosition.position;
        }
    }

    IEnumerator LoadNewDay(int dayToLoad)
    {
        Scene previousActiveScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync("Loading Screen", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.2f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Loading Screen"));
        foreach(Transform child in GameObject.Find("Days").transform)
        {
            child.gameObject.SetActive(true);
        }

        GameObject dayToLoadObject = GameObject.Find("Day " + dayToLoad.ToString());
        dayToLoadObject.SetActive(true);

        foreach(Transform child in GameObject.Find("Days").transform)
        {
            if (child.name != dayToLoadObject.name)
            {
                child.gameObject.SetActive(false);
            }
        }
        SceneManager.UnloadSceneAsync(previousActiveScene);

        yield break;
    }

    public enum Scenes
    {
        Office, HospitalHall, Archive, HospitalExit,
        Room, HouseHall, Kitchen,
        None
    }
    public enum DoorSounds
    {
        Door, WayIn, Stairs
    } 
}
