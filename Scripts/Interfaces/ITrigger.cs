using System;

public interface ITrigger
{
    public event Action OnTriggerActionMade;

    public void TriggerAction();
}