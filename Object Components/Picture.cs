using System.Collections;
using UnityEngine;

public class Picture : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cursedImage, idleImage;

    private CurseHandler curseHandler => FindObjectOfType<CurseHandler>();

    private void OnEnable() => StartCoroutine(CheckForCurse());

    private void Curse()
    {
        idleImage.gameObject.SetActive(false);
        cursedImage.gameObject.SetActive(true);
    }
    private void BackToIdle()
    {   
        idleImage.gameObject.SetActive(true);
        cursedImage.gameObject.SetActive(false);
    }

    private IEnumerator CheckForCurse()
    {
        yield return new WaitForEndOfFrame();
        
        if (curseHandler.IsWorldCursed)
            Curse();
        else
            BackToIdle();

        yield break;
    }
}