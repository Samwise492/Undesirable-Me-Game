using UnityEngine;

public class TriggerToActivateAudioSourceByDialogue : DialogueTrigger
{
    [Header("Manipulated object")]
    [SerializeField]
    private AudioSource audioSource;

    protected override void Start()
    {
        base.Start();

        audioSource.enabled = false;
    }

    public override void TriggerAction()
    {
        audioSource.enabled = true;

        EndTrigger();
    }
}