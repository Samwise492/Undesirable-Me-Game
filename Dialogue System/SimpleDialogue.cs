using UnityEngine;

public class SimpleDialogue : BaseDialogue
{
    [SerializeField]
    private DialogueData dialogueData;

    protected override void Start()
    {
        base.Start();

        OnDialogueFinished += DoEnd;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        OnDialogueFinished -= DoEnd;
    }

    public override void Play()
    {
        PlayDialogue(dialogueData);
    }

    private void DoEnd(DialogueData data)
    {
        NotifyEnd();
    }
}