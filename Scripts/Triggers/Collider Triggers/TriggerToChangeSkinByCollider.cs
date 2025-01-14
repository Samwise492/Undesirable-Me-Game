using UnityEngine;

public class TriggerToChangeSkinByCollider : ColliderTrigger
{
    [SerializeField]
    private SkinChanger skinChanger;

    private void Start()
    {
        if (PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id))
        {
            TriggerAction();
        }
    }

    public override void TriggerAction()
    {
        skinChanger.ChangeSkin();

        EndTrigger();
    }
}
