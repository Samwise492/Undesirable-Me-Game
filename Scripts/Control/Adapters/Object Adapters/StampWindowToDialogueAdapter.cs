using UnityEngine;

public class StampWindowToDialogueAdapter : MonoBehaviour
{
	[SerializeField]
	private BaseDialogue dialogue;

	private StampWindow stampWindow => FindObjectOfType<StampWindow>();

    private void OnEnable()
    {
        stampWindow.OnStampWindowStateChanged += CheckState;
        dialogue.OnDialogueFinished += SetInteractable;
    }
    private void OnDisable()
    {
        if (stampWindow)
        {
            stampWindow.OnStampWindowStateChanged -= CheckState;
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
            dialogue.Play();
        }
    }

    private void SetInteractable(DialogueData dialogueData)
    {
        stampWindow.SetInteractable(true);
    }
}
