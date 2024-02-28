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
        if (player.gameObject.transform.position.x < livingPicture.LeftBorder.position.x)
            livingPicture.playerPosition = LivingPicture.PlayerPosition.left;
        else if (player.gameObject.transform.position.x > livingPicture.LeftBorder.position.x && player.gameObject.transform.position.x < livingPicture.RightBorder.position.x)
            livingPicture.playerPosition = LivingPicture.PlayerPosition.centre;
        else if (player.gameObject.transform.position.x > livingPicture.RightBorder.position.x)
            livingPicture.playerPosition = LivingPicture.PlayerPosition.right;
    }
}
