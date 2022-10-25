using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPicture : MonoBehaviour
{
    SpriteRenderer[] cursedImages = new SpriteRenderer[3];
    Transform leftPoint, rightPoint;
    Player player;
    [SerializeField] protected CurseHandler curseHandler;
    protected SpriteRenderer regularImage;

    void Awake()
    {
        regularImage = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        for (int i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
        {
            gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            cursedImages[i] = gameObject.transform.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            gameObject.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        leftPoint = transform.GetChild(3).GetChild(0);
        rightPoint = transform.GetChild(3).GetChild(1);
        player = FindObjectOfType<Player>();
    }
    void OnEnable()
    {
        StartCoroutine(CheckForCurse());
    }
    void Curse()
    {
        regularImage.gameObject.SetActive(false);
        cursedImages[0].transform.parent.gameObject.SetActive(true);
        StartCoroutine(RunningEyes());
    }
    void Restore()
    {   
        regularImage.gameObject.SetActive(true);
        cursedImages[0].transform.parent.gameObject.SetActive(false);
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
    IEnumerator RunningEyes()
    {
        while (true)
        {
            if (player.gameObject.transform.position.x > leftPoint.position.x && player.gameObject.transform.position.x < rightPoint.position.x)
            {
                foreach (var image in cursedImages)
                {
                    image.gameObject.SetActive(false);
                }
                cursedImages[1].gameObject.SetActive(true);
            }
            else if (player.gameObject.transform.position.x < leftPoint.position.x)
            {
                foreach (var image in cursedImages)
                {
                    image.gameObject.SetActive(false);
                }
                cursedImages[0].gameObject.SetActive(true);
            }
            else if (player.gameObject.transform.position.x > rightPoint.position.x)
            {
                foreach (var image in cursedImages)
                {
                    image.gameObject.SetActive(false);
                }
                cursedImages[2].gameObject.SetActive(true);
            }

            if (!curseHandler.isWorldCursed)
            {
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
