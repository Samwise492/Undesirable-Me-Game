using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingPicture : MonoBehaviour
{
    Transform leftBorder, rightBorder;
    Transform changingImagesNest;
    CurseHandler curseHandler;
    SpriteRenderer[] changingImages = new SpriteRenderer[3];
    SpriteRenderer regularImage;
    Player player;

    void Awake()
    {
        curseHandler = GameObject.FindObjectOfType<CurseHandler>();
        regularImage = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        changingImagesNest = gameObject.transform.GetChild(0);

        for (int i = 0; i < changingImagesNest.childCount; i++)
        {
            changingImagesNest.GetChild(i).gameObject.SetActive(true);
            changingImages[i] = gameObject.transform.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            changingImagesNest.GetChild(i).gameObject.SetActive(false);
        }

        leftBorder = transform.GetChild(3).GetChild(0);
        rightBorder = transform.GetChild(3).GetChild(1);
        player = FindObjectOfType<Player>();
    }
    void OnEnable() => StartCoroutine(CheckForCurse());

    void Curse()
    {
        regularImage.gameObject.SetActive(false);
        changingImagesNest.gameObject.SetActive(true);
        StartCoroutine(RunningEyes());
    }
    void BackToNormal()
    {   
        regularImage.gameObject.SetActive(true);
        changingImagesNest.gameObject.SetActive(false);
    }

    IEnumerator CheckForCurse()
    {
        yield return new WaitForEndOfFrame();
        
        if (curseHandler.isWorldCursed)
            Curse();
        else
            BackToNormal();
        
        yield break;
    }
    IEnumerator RunningEyes()
    {
        while (true)
        {
            if (player.gameObject.transform.position.x < leftBorder.position.x)
            {
                foreach (var image in changingImages)
                {
                    image.gameObject.SetActive(false);
                }
                changingImages[0].gameObject.SetActive(true);
            }
            else if (player.gameObject.transform.position.x > leftBorder.position.x && player.gameObject.transform.position.x < rightBorder.position.x)
            {
                foreach (var image in changingImages)
                {
                    image.gameObject.SetActive(false);
                }
                changingImages[1].gameObject.SetActive(true);
            }
            else if (player.gameObject.transform.position.x > rightBorder.position.x)
            {
                foreach (var image in changingImages)
                {
                    image.gameObject.SetActive(false);
                }
                changingImages[2].gameObject.SetActive(true);
            }

            if (!curseHandler.isWorldCursed)
                yield break;
                
            yield return new WaitForSeconds(1);
        }
    }
}
