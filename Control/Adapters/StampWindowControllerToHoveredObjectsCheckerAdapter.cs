using UnityEngine;

public class StampWindowControllerToHoveredObjectsCheckerAdapter : MonoBehaviour
{
	private StampWindowController stampWindowController => FindObjectOfType<StampWindowController>();

	private HoveredObjectsChecker objectsChecker => FindAnyObjectByType<HoveredObjectsChecker>();

    private void OnEnable()
    {
        objectsChecker.OnHovered += ShowHint;
    }
    private void OnDisable()
    {
        if (objectsChecker)
        {
            objectsChecker.OnHovered -= ShowHint;
        }
    }

    private void ShowHint(GameObject go)
    {
        if (stampWindowController.GetStampWindowState())
        {
            stampWindowController.HandleHintState(go);
        }
    }
}
