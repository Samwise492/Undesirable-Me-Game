using UnityEngine;

public class CurseHandler : MonoBehaviour
{
    public bool IsWorldCursed => isWorldCursed;

    private Door[] doors => Resources.FindObjectsOfTypeAll<Door>();

    private System.Random randomiser = new System.Random();
    private bool isWorldCursed;

    private void Start()
    {
        foreach (Door door in doors)
        {
            door.OnSceneChange.AddListener(RandomCurse);
        }
    }
    private void OnDestroy()
    {
        foreach (Door door in doors)
        {
            door.OnSceneChange.RemoveAllListeners();
        }
    }

    private void RandomCurse()
    {
        int randomValue = randomiser.Next(4);

        if (randomValue == 1)
            isWorldCursed = true;
        else
            isWorldCursed = false;
    }
}