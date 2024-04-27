using UnityEngine;

public class TriggerToChangeGameObjectsByDialogue : DialogueTrigger
{
	[SerializeField]
	private GameObject[] objectsToOff;

	[SerializeField]
	private GameObject[] objectsToOn;

    public override void TriggerAction()
    {
        foreach (GameObject obj in objectsToOff)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectsToOn)
        {
            obj.SetActive(true);
        }
    }
}