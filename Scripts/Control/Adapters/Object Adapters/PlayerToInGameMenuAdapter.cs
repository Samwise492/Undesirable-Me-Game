using UnityEngine;

public class PlayerToInGameMenuAdapter : MonoBehaviour
{
    private Player player => FindObjectOfType<Player>();
    private InGameMenu inGameMenuManager => FindObjectOfType<InGameMenu>();

    private void OnEnable()
    {
        if (player)
        {
            inGameMenuManager.OnInGameMenuSet += SetPlayerMovement;
        }
    }
    private void OnDisable()
    {
        if (player)
        {
            if (inGameMenuManager)
            {
                inGameMenuManager.OnInGameMenuSet -= SetPlayerMovement;
            }
        }
    }

    private void SetPlayerMovement(bool state)
    {
        if (UIManager.Instance.GetActiveWindow() != null)
        {
            return;
        }

        if (state)
        {
            player.ProhibitMovement();
        }
        else
        {
            player.AllowMovement();
        }
    }
}