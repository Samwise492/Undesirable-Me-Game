using System;
using System.Collections;
using UnityEngine;

public class LivingPicture : BasePicture
{
    [SerializeField]
    private PictureMarker[] cursedPictures;

    [Space]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public FollowedObjectPosition objectPosition;
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public FollowedObjectSpriteFlip objectSpriteFlip;

    [SerializeField]
    private SpriteRenderer objectToFollow;

    [Header("Borders")]
    [SerializeField]
    private Transform leftBorder;
    [SerializeField]
    private Transform rightBorder;

    [Header("Settings")]
    [SerializeField]
    private bool isGoToIdleWhenSomeoneWatching;

    private void OnDisable()
    {
        if (CurseManager.Instance.isWorldCursed)
        {
            SetCentrePicture();
        }
    }

    private void Update()
    {
        DetectPlayerPosition();
    }

    protected override void Curse()
    {
        idleImage.gameObject.SetActive(false);

        StartCoroutine(PlayCursedAnimation());
    }
    protected override void BackToIdle()
    {
        idleImage.gameObject.SetActive(true);

        RefreshCursedImages();
    }

    private void DetectPlayerPosition()
    {
        float playerXPosition = objectToFollow.transform.position.x;
        float leftBorderXPosition = leftBorder.position.x;
        float rightBorderXPosition = rightBorder.position.x;

        if (playerXPosition < leftBorderXPosition)
        {
            objectPosition = FollowedObjectPosition.left;
        }
        else if (playerXPosition > leftBorderXPosition && playerXPosition < rightBorderXPosition)
        {
            objectPosition = FollowedObjectPosition.centre;
        }
        else if (playerXPosition > rightBorderXPosition)
        {
            objectPosition = FollowedObjectPosition.right;
        }
    }

    private void RefreshCursedImages()
    {
        foreach (PictureMarker picture in cursedPictures)
        {
            picture.Picture.gameObject.SetActive(false);
        }
    }

    private void SetLeftPicture()
    {
        RefreshCursedImages();
        cursedPictures[0].Picture.gameObject.SetActive(true);
    }
    private void SetCentrePicture()
    {
        RefreshCursedImages();
        cursedPictures[1].Picture.gameObject.SetActive(true);
    }
    private void SetRightPicture()
    {
        RefreshCursedImages();
        cursedPictures[2].Picture.gameObject.SetActive(true);
    }

    private IEnumerator PlayCursedAnimation()
    {
        while (true)
        {
            objectSpriteFlip = objectToFollow.flipX ? FollowedObjectSpriteFlip.left : FollowedObjectSpriteFlip.right;

            if (objectPosition == FollowedObjectPosition.left)
            {
                if (isGoToIdleWhenSomeoneWatching)
                {
                    if (objectSpriteFlip != FollowedObjectSpriteFlip.right)
                    {
                        SetLeftPicture();
                    }
                    else
                    {
                        SetCentrePicture();
                    }
                }
                else
                {
                    SetLeftPicture();
                }
            }
            else if (objectPosition == FollowedObjectPosition.centre)
            {
                SetCentrePicture();
            }
            else if (objectPosition == FollowedObjectPosition.right)
            {
                if (isGoToIdleWhenSomeoneWatching)
                {
                    if (objectSpriteFlip != FollowedObjectSpriteFlip.left)
                    {
                        SetRightPicture();
                    }
                    else
                    {
                        SetCentrePicture();
                    }
                }
                else
                {
                    SetRightPicture();
                }
            }

            if (!CurseManager.Instance.isWorldCursed)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public enum FollowedObjectPosition
    {
        left,
        centre,
        right
    }
    public enum FollowedObjectSpriteFlip
    {
        left,
        right
    }

    [Serializable]
    private class PictureMarker
    {
        public FollowedObjectPosition Position => position;
        public SpriteRenderer Picture => picture;

        [Tooltip("Position relative to player")]
        [SerializeField]
        private FollowedObjectPosition position;
        [SerializeField]
        private SpriteRenderer picture;
    }
}