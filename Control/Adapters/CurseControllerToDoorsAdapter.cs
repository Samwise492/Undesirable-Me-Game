using UnityEngine;

public class CurseControllerToDoorsAdapter : MonoBehaviour
{
    private CurseController curseController => FindObjectOfType<CurseController>(true);

    private Door[] doors => FindObjectsOfType<Door>(true);

    private void OnEnable()
    {
        if (curseController)
        {
            foreach (Door door in doors)
            {
                door.OnStageChanged += curseController.RandomiseCurse;
            }
        }
    }
    private void OnDisable()
    {
        if (curseController)
        {
            foreach (Door door in doors)
            {
                door.OnStageChanged -= curseController.RandomiseCurse;
            }
        }
    }
}
