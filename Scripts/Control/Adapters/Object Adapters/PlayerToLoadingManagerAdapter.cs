using UnityEngine;

public class PlayerToLoadingManagerAdapter : MonoBehaviour
{
    private Player player => FindObjectOfType<Player>();
    private LoadingManager loadingManager => FindObjectOfType<LoadingManager>();

    private void OnEnable()
    {
        if (player)
        {
            loadingManager.OnLoading += ProhibitMovement;
        }
    }
    private void OnDisable()
    {
        if (player)
        {
            if (loadingManager)
            {
                loadingManager.OnLoading -= ProhibitMovement;
            }
        }
    }

    private void ProhibitMovement()
    {
        if (player)
        {
            player.ProhibitMovement();
        }
    }
}