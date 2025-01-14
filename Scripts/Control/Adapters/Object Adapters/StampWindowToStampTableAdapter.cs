using UnityEngine;

public class StampWindowToStampTableAdapter : MonoBehaviour
{
    private StampWindow stampWindow => FindObjectOfType<StampWindow>();

    private StampTable stampTable => FindObjectOfType<StampTable>(true);

    private void OnEnable()
    {
        if (stampTable)
        {
            stampWindow.realStampButton.onClick.AddListener(stampTable.StampRealStamp);
            stampWindow.fakeStampButton.onClick.AddListener(stampTable.StampFakeStamp);
            stampTable.OnPlayerInteracted += TurnOnStampWindow;
            stampTable.OnStamped += TurnOffStampWindow;
        }
    }
    private void OnDisable()
    {
        if (stampTable && stampWindow)
        {
            stampWindow.realStampButton.onClick.RemoveListener(stampTable.StampRealStamp);
            stampWindow.fakeStampButton.onClick.RemoveListener(stampTable.StampFakeStamp);
            stampTable.OnPlayerInteracted -= TurnOnStampWindow;
            stampTable.OnStamped -= TurnOffStampWindow;
        }
    }

    private void TurnOnStampWindow()
    {
        stampWindow.SetStampWindowState(true);
    }

    private void TurnOffStampWindow()
    {
        stampWindow.SetStampWindowState(false);
        stampWindow.SetInteractable(false);
    }
}
