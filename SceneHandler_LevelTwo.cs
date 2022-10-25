using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler_LevelTwo : MonoBehaviour
{
    public GameObject roomScene;
    public GameObject houseHallScene;
    public GameObject kitchenScene;

    void Awake() 
    {
        try
        {
            DialogueInitialisation.Initialise();
        }
        catch (System.ArgumentException)
        {

        }
    }
}
