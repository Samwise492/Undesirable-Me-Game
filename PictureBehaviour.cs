using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureBehaviour : MonoBehaviour
{
    [SerializeField] protected CurseHandler curseHandler;
    protected SpriteRenderer cursedImage, regularImage;

    void Awake()
    {
        cursedImage = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        regularImage = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        StartCoroutine(CheckForCurse());
    }

    void Curse()
    {
        regularImage.gameObject.SetActive(false);
        cursedImage.gameObject.SetActive(true);
    }
    void Restore()
    {   
        regularImage.gameObject.SetActive(true);
        cursedImage.gameObject.SetActive(false);
    }
    IEnumerator CheckForCurse()
    {
        yield return new WaitForEndOfFrame();
        
        if (curseHandler.isWorldCursed)
        {
            Curse();
        }
        else
            Restore();

        yield break;
    }
}
