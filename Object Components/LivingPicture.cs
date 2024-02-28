using System.Collections;
using UnityEngine;

public class LivingPicture : MonoBehaviour
{
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public bool isCursed;

    [HideInInspector]
    public PlayerPosition playerPosition;

    public Transform LeftBorder => leftBorder;
    public Transform RightBorder => rightBorder;

    [SerializeField]
    private Transform leftBorder;
    [SerializeField]
    private Transform rightBorder;

    [Space][SerializeField] 
    private Transform cursedStatesNest;
    [SerializeField] 
    private SpriteRenderer idleState;

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
        
        if (isCursed)
            Curse();
        else
            BackToIdle();
        
        yield break;
    }

    private IEnumerator PlayCursedAnimation()
    {
        while (true)
        {
            switch (playerPosition)
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

            if (!isCursed)
                yield break;
                
            yield return new WaitForSeconds(1);
        }
    }

    public enum PlayerPosition
    {
        left,
        centre,
        right
    }
}