using UnityEngine;

public class PlayerPositionChanger : MonoBehaviour
{
    private void Start()
    {
        if (GameState.Instance.CurrentState == CurrentGameState.IsLoadStageFromMenu)
        {
            LoadPlayerPosition();
        }
    }

    private void LoadPlayerPosition()
    {
        Vector3 newPosition = new()
        {
            x = PlayerSaveLoadProvider.Instance.GetCurrentSave().playerPositionX,
            y = PlayerSaveLoadProvider.Instance.GetCurrentSave().playerPositionY,
            z = PlayerSaveLoadProvider.Instance.GetCurrentSave().playerPositionZ
        };

        FindObjectOfType<Player>().transform.localPosition = newPosition;
    }
}
