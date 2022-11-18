using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Storage of in-level scenes
public class SceneHandler_LevelThree : MonoBehaviour
{
    public GameObject officeScene;

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
