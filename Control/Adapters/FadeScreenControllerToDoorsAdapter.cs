using UnityEngine;

public class FadeScreenControllerToDoorsAdapter : MonoBehaviour
{
	private FadeScreenController fadeScreenController => FindObjectOfType<FadeScreenController>();
	private Door[] doors => FindObjectsOfType<Door>(true);

    private void OnEnable()
    {
        foreach (Door door in doors)
        {
            door.OnChangeStageWithFade += delegate { SwitchStageWithFade(door); };
        }
    }
    private void OnDisable()
    {
        foreach (Door door in doors)
        {
            door.OnChangeStageWithFade -= delegate { SwitchStageWithFade(door); };
        }
    }

    private void SwitchStageWithFade(Door door)
    {
        fadeScreenController.OnFading -= door.SwitchStage;
        fadeScreenController.OnFading += door.SwitchStage;

        fadeScreenController.FadeScreenInAndOut();
    }
}
