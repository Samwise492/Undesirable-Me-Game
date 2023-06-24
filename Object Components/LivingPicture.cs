using System.Collections;
using UnityEngine;

public class LivingPicture : MonoBehaviour
{
    [SerializeField] private Transform leftBorder, rightBorder;

    [Space]
    [SerializeField] private Transform cursedStatesNest;
    [SerializeField] private SpriteRenderer idleState;

    private CurseHandler curseHandler => FindObjectOfType<CurseHandler>();
    private Player player => FindObjectOfType<Player>();

    private SpriteRenderer[] cursedStates = new SpriteRenderer[3];

    private void Awake()
    {
        for (int i = 0; i < cursedStatesNest.childCount; i++)
        {
            cursedStatesNest.GetChild(i).gameObject.SetActive(true);
            cursedStates[i] = gameObject.transform.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            cursedStatesNest.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnEnable() => StartCoroutine(CheckForCurse());

    private PlayerPosition? GetPlayerPosition()
    {
        if (player.gameObject.transform.position.x < leftBorder.position.x)
            return PlayerPosition.left;
        else if (player.gameObject.transform.position.x > leftBorder.position.x && player.gameObject.transform.position.x < rightBorder.position.x)
            return PlayerPosition.centre;
        else if (player.gameObject.transform.position.x > rightBorder.position.x)
            return PlayerPosition.right;

        return null;
    }

    private void Curse()
    {
        idleState.gameObject.SetActive(false);
        cursedStatesNest.gameObject.SetActive(true);

        StartCoroutine(PlayCursedAnimation());
    }
    private void BackToIdle()
    {   
        idleState.gameObject.SetActive(true);
        cursedStatesNest.gameObject.SetActive(false);
    }

    private void TurnOffCursedImages()
    {
        foreach (SpriteRenderer image in cursedStates)
        {
            image.gameObject.SetActive(false);
        }
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
    private IEnumerator PlayCursedAnimation()
    {
        while (true)
        {
            switch (GetPlayerPosition())
            {
                case PlayerPosition.left:
                    TurnOffCursedImages();
                    cursedStates[0].gameObject.SetActive(true);
                    break;
                case PlayerPosition.centre:
                    TurnOffCursedImages();
                    cursedStates[1].gameObject.SetActive(true);
                    break;
                case PlayerPosition.right:
                    TurnOffCursedImages();
                    cursedStates[2].gameObject.SetActive(true);
                    break;
            }

            if (!curseHandler.IsWorldCursed)
                yield break;
                
            yield return new WaitForSeconds(1);
        }
    }

    private enum PlayerPosition
    {
        left,
        centre,
        right
    }
}