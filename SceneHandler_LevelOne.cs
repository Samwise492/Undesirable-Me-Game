using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Storage of in-level scenes
public class SceneHandler_LevelOne : MonoBehaviour
{
    public GameObject officeScene;
    public GameObject hospitalHallScene;
    public GameObject archiveScene;

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
