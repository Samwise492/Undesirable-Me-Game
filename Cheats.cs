using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] bool isDoorUnlocked;
    bool startValue;
    DoorBehaviour[] doorBehaviourObjects;

    void Awake() => startValue = isDoorUnlocked;
    void OnValidate()
    {
        if (Application.isPlaying) 
        {
            if (isDoorUnlocked != startValue)
            {
                doorBehaviourObjects = Resources.FindObjectsOfTypeAll<DoorBehaviour>();
                foreach (var component in doorBehaviourObjects)
                {
                    component.isLocked = !isDoorUnlocked;
                }
            }
        }
    }
}
