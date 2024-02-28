using System.Collections;
using UnityEngine;

public class TriggerToActivateDialogue : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField] 
    private Dialogue dialogueTrigger;

    [Header("Manipulated object")]
    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private ChoiceMaker choiceMaker;

    private void Start()
    {
        dialogue.enabled = false;

        dialogueTrigger.OnDialogueFinished += Execute;
    }
    private void OnDestroy()
    {
        dialogueTrigger.OnDialogueFinished -= Execute;
    }

    private void Execute(DialogueData dialogueData)
    {
        StartCoroutine(CheckForDialogueWindowClosure());
    }

    private IEnumerator CheckForDialogueWindowClosure()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (UIManager.Instance.GetActiveWindow() == null)
            {
                dialogue.enabled = true;

                if (choiceMaker)
                {
                    choiceMaker.enabled = true;
                }

                yield break;
            }
        }
    }
}
