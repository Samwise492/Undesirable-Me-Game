using UnityEngine;

public class TriggerToChangeGameObjectsBySkinChanger : BaseTrigger
{
    [SerializeField]
    private SkinChanger skinChanger;

    [SerializeField]
    private GameObject[] objectsToOff;

    [SerializeField]
    private GameObject[] objectsToOn;

    private void Start()
    {
        if (PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id))
        {
            TriggerAction();
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
        foreach (GameObject obj in objectsToOff)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectsToOn)
        {
            obj.SetActive(true);
        }

        EndTrigger();
    }
}
