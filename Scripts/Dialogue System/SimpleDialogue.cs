using UnityEngine;

public class SimpleDialogue : BaseDialogue
{
    [SerializeField]
    private DialogueData dialogueData;

    protected override void Start()
    {
        if (LogDataManager.Instance.GetPassedDialogues().Contains(dialogueData))
        {
            enabled = false;
            return;
        }

        base.Start();

        OnDialogueFinished += DoEnd;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        OnDialogueFinished -= DoEnd;
    }

    public override DialogueData GetDialogue()
    {
        return dialogueData;
    }
    public override void InjectDialogueData(DialogueData dialogueData)
    {
        this.dialogueData = dialogueData;
    }
    protected override void AbstractPlay()
    {
        PlayDialogue(dialogueData);
    }

    private void DoEnd(DialogueData data)
    {
        NotifyDialogueEnd();
    }
}