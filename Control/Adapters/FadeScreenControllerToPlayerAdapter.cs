using UnityEngine;

public class FadeScreenControllerToPlayerAdapter : MonoBehaviour
{
	private FadeScreenController controller => FindObjectOfType<FadeScreenController>();

	private Player player => FindObjectOfType<Player>();

    private void OnEnable()
    {
        controller.OnStartFading += player.ProhibitMovement;
    }
    private void OnDisable()
    {
        if (controller && player)
        controller.OnStartFading -= player.ProhibitMovement;
    }
}
