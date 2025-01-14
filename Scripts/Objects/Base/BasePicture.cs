using System.Collections;
using UnityEngine;

public abstract class BasePicture : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer idleImage;

    protected abstract void Curse();
    protected abstract void BackToIdle();

    private void OnEnable()
    {
        StartCoroutine(CheckForCurse());
    }

    private IEnumerator CheckForCurse()
    {
        yield return new WaitForEndOfFrame();

        if (CurseManager.Instance.isWorldCursed)
        {
            Curse();
        }
        else
        {
            BackToIdle();
        }

        yield break;
    }
}