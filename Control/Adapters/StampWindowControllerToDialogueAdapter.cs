using UnityEngine;

public class StampWindowControllerToDialogueAdapter : MonoBehaviour
{
	[SerializeField]
	private Dialogue dialogue;

	private StampWindowController stampWindowController => FindObjectOfType<StampWindowController>();

    private void OnEnable()
    {
        stampWindowController.OnStampWindowStateChanged += CheckState;
        dialogue.OnDialogueFinished += SetInteractable;
    }
    private void OnDisable()
    {
        if (stampWindowController)
        {
            stampWindowController.OnStampWindowStateChanged -= CheckState;
        }

        if (dialogue)
        {
            dialogue.OnDialogueFinished -= SetInteractable;
        }
    }

    private void CheckState(bool state)
    {
        if (state)
        {
            dialogue.PlayDialogue();
        }
    }

    private void SetInteractable(DialogueData dialogueData)
    {
        stampWindowController.SetInteractable(true);
    }
}
