using UnityEngine;

public class TriggerToActivateDialogueBySkinChanger : BaseTrigger
{
    [SerializeField]
    private SkinChanger skinChanger;

    [SerializeField]
    private BaseDialogue dialogue;

    private void Start()
    {
        if (PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id))
        {
            return;
        }

        skinChanger.OnSkinChanged += TriggerAction;
    }
    private void OnDestroy()
    {
        skinChanger.OnSkinChanged -= TriggerAction;
    }

    public override void TriggerAction()  
    {
        dialogue.Play();

        EndTrigger();
    }
}
