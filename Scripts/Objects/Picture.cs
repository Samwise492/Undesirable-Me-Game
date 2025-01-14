using UnityEngine;

public class Picture : BasePicture
{
    [SerializeField]
    private SpriteRenderer cursedImage;

    protected override void Curse()
    {
        idleImage.gameObject.SetActive(false);
        cursedImage.gameObject.SetActive(true);
    }
    protected override void BackToIdle()
    {
        idleImage.gameObject.SetActive(true);
        cursedImage.gameObject.SetActive(false);
    }
}