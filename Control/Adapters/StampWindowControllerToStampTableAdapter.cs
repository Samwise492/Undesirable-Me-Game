using UnityEngine;

public class StampWindowControllerToStampTableAdapter : MonoBehaviour
{
    private StampWindowController stampWindowController => FindObjectOfType<StampWindowController>();

    private StampTable stampTable => FindObjectOfType<StampTable>(true);

    private void OnEnable()
    {
        if (stampTable)
        {
            stampWindowController.realStampButton.onClick.AddListener(stampTable.StampRealStamp);
            stampWindowController.fakeStampButton.onClick.AddListener(stampTable.StampFakeStamp);
            stampTable.OnPlayerInteracted += TurnOnStampWindow;
            stampTable.OnStamped += TurnOffStampWindow;
        }
    }
    private void OnDisable()
    {
        if (stampTable && stampWindowController)
        {
            stampWindowController.realStampButton.onClick.RemoveListener(stampTable.StampRealStamp);
            stampWindowController.fakeStampButton.onClick.RemoveListener(stampTable.StampFakeStamp);
            stampTable.OnPlayerInteracted -= TurnOnStampWindow;
            stampTable.OnStamped -= TurnOffStampWindow;
        }
    }

    private void TurnOnStampWindow()
    {
        stampWindowController.SetStampWindowState(true);
    }

    private void TurnOffStampWindow()
    {
        stampWindowController.SetStampWindowState(false);
    }
}
