using UnityEngine;

public class TriggerToActivateDoorByDialogue : DialogueTrigger
{
    [Header("Manipulated object")]
    [SerializeField]
    private Door door;

    protected override void Start()
    {
        base.Start();

        door.enabled = false;
    }

    public override void TriggerAction()
    {
        door.enabled = true;
    }
}