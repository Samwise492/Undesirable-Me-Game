using NaughtyAttributes;
using System;
using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour, ITrigger
{
    public event Action OnTriggerActionMade;

    public event Action<BaseTrigger> OnRequestFade;

    public bool IsActionOnEnd => isActionOnEnd;
    public bool IsActionOnProcess => isActionOnProcess;

    [SerializeField]
    private bool isFadeBeforeAction;

    [ShowIf("isFadeBeforeAction")]
    [SerializeField]
    protected bool isFadeBeforeSceneLoad;

    [ShowIf("isFadeBeforeAction")]
    [SerializeField]
    private bool isActionOnEnd;

    [ShowIf("isFadeBeforeAction")]
    [SerializeField]
    private bool isActionOnProcess;

    public abstract void TriggerAction();

    protected void EndTrigger()
    {
        OnTriggerActionMade?.Invoke();
    }

    protected void CheckTriggerBehaviour()
    {
        if (isFadeBeforeAction)
        {
            OnRequestFade?.Invoke(this);
        }
        else
        {
            TriggerAction();
        }
    }
}