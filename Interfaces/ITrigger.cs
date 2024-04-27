using System;

public interface ITrigger
{
    public event Action OnTriggerActionMade;

    public abstract void TriggerAction();
}