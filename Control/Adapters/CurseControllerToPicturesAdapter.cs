using UnityEngine;

public class CurseControllerToPicturesAdapter : MonoBehaviour
{
	private CurseController curseController => FindObjectOfType<CurseController>();

	private LivingPicture[] livingPictures => FindObjectsOfType<LivingPicture>(true);
	private Picture[] pictures => FindObjectsOfType<Picture>(true);

    private void OnEnable()
    {
        if (curseController)
        {
            curseController.OnCursed += Curse;
        }
    }
    private void OnDisable()
    {
        if (curseController)
        {
            curseController.OnCursed -= Curse;
        }
    }

    private void Curse(bool isCursed)
    {
        foreach (LivingPicture livingPicture in livingPictures)
        {
            livingPicture.isCursed = isCursed;
        }
        foreach (Picture picture in pictures)
        {
            picture.isCursed = isCursed;
        }
    }

    private void Update()
    {
        if (curseController)
        {
            //foreach (LivingPicture livingPicture in livingPictures)
            //{
            //    livingPicture.isCursed = curseController.IsWorldCursed;
            //}
            //foreach (Picture picture in pictures)
            //{
            //    picture.isCursed = curseController.IsWorldCursed;
            //}
        }
    }
}
