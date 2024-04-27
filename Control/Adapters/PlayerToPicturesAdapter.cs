using UnityEngine;

public class PlayerToPicturesAdapter : MonoBehaviour
{
    private Player player => FindObjectOfType<Player>();

    private LivingPicture[] livingPictures => FindObjectsOfType<LivingPicture>(true);

    private void Update()
    {
        foreach (LivingPicture livingPicture in livingPictures)
            if (livingPicture.gameObject.activeInHierarchy)
                DetectPlayerPosition(livingPicture);
    }

    private void DetectPlayerPosition(LivingPicture livingPicture)
    {
        float playerXPosition = player.gameObject.transform.position.x;
        float leftBorderXPosition = livingPicture.LeftBorder.position.x;
        float rightBorderXPosition = livingPicture.LeftBorder.position.x;

        if (playerXPosition < leftBorderXPosition)
        {
            livingPicture.playerPosition = LivingPicture.PlayerPosition.left;
        }
        else if (playerXPosition > leftBorderXPosition && playerXPosition < rightBorderXPosition)
        {
            livingPicture.playerPosition = LivingPicture.PlayerPosition.centre;
        }
        else if (playerXPosition > rightBorderXPosition)
        {
            livingPicture.playerPosition = LivingPicture.PlayerPosition.right;
        }
    }
}
