using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseHandler : MonoBehaviour
{
    public bool isWorldCursed;
    ChangeScenes[] doors;
    System.Random randomiser;

    void Start()
    {
        doors = Resources.FindObjectsOfTypeAll<ChangeScenes>();
        randomiser = new System.Random();

        StartCoroutine(RandomCurse());
    }
    IEnumerator RandomCurse()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            foreach (var door in doors)
            {
                if (door.isSceneChanged == true)
                {
                    door.isSceneChanged = false;

                    int randomValue = randomiser.Next(4);
                    if (randomValue == 1)
                        isWorldCursed = true;
                    else
                        isWorldCursed = false;
                }
            }
        }
    }
}
