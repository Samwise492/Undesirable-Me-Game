using System;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public event Action OnSkinChanged;

    [SerializeField]
    private bool isChangeOnce;

    [SerializeField]
    private Animator animatorToChangeState;

    [SerializeField]
    private string animatorBoolName;

    private bool isSkinChanged;

    public void ChangeSkin()
    {
        if (isChangeOnce)
        {
            if (isSkinChanged)
            {
                return;
            }
        }

        animatorToChangeState.SetBool(animatorBoolName, true);

        if (isChangeOnce)
        {
            isSkinChanged = true;

            OnSkinChanged?.Invoke();
        }
        else
        {
            isSkinChanged = !isSkinChanged;

            OnSkinChanged?.Invoke();
        }
    }
}
