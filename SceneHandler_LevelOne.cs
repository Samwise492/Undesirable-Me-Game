using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler_LevelOne : MonoBehaviour
{
    public GameObject officeScene;
    public GameObject hospitalHallScene;
    public GameObject archiveScene;

    void Awake() 
    {
        DialogueInitialisation.Initialise();
    }
}
