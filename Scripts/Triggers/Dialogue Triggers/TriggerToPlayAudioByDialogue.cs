using UnityEngine;

public class TriggerToPlayAudioByDialogue : DialogueTrigger
{
	[SerializeField]
	private AudioSource audioSource;

    public override void TriggerAction()
    {
        audioSource.Play();

        EndTrigger();
    }
}