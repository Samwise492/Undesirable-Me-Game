using System.Collections;
using UnityEngine;

public class Picture : MonoBehaviour
{
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public bool isCursed;

    [SerializeField]
    private SpriteRenderer cursedImage;
    [SerializeField]
    private SpriteRenderer idleImage;

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
        
        if (isCursed)
            Curse();
        else
            BackToIdle();

        yield break;
    }
}