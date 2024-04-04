using System;
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

    [Space]
    [SerializeField]
    private PictureMarker[] cursedPictures;
    [SerializeField] 
    private SpriteRenderer idleState;

    private void OnEnable()
    {
        StartCoroutine(CheckForCurse());
    }

    private void Curse()
    {
        idleState.gameObject.SetActive(false);

        StartCoroutine(PlayCursedAnimation());
    }
    private void BackToIdle()
    {   
        idleState.gameObject.SetActive(true);

        RefreshCursedImages();
    }

    private void RefreshCursedImages()
    {
        foreach (PictureMarker picture in cursedPictures)
        {
            picture.Picture.gameObject.SetActive(false);
        }
    }

    private IEnumerator CheckForCurse()
    {
        yield return new WaitForEndOfFrame();

        if (isCursed)
        {
            Curse();
        }
        else
        {
            BackToIdle();
        }
        
        yield break;
    }

    private IEnumerator PlayCursedAnimation()
    {
        while (true)
        {
            switch (playerPosition)
            {
                case PlayerPosition.left:
                    RefreshCursedImages();
                    cursedPictures[0].Picture.gameObject.SetActive(true);
                    break;
                case PlayerPosition.centre:
                    RefreshCursedImages();
                    cursedPictures[1].Picture.gameObject.SetActive(true);
                    break;
                case PlayerPosition.right:
                    RefreshCursedImages();
                    cursedPictures[2].Picture.gameObject.SetActive(true);
                    break;
            }

            if (!isCursed)
            {
                yield break;
            }
                
            yield return new WaitForSeconds(1);
        }
    }

    public enum PlayerPosition
    {
        left,
        centre,
        right
    }

    [Serializable]
    private class PictureMarker
    {
        public PlayerPosition Position => position;
        public SpriteRenderer Picture => picture;

        [Tooltip("Position relative to player")]
        [SerializeField]
        private PlayerPosition position;
        [SerializeField]
        private SpriteRenderer picture;
    }
}