using System;
using UnityEngine;

[RequireComponent(typeof (Door))]
public class SpecificDoorBehaviour : MonoBehaviour
{
    public event Action OnTriedToOpenClosedDoor;

    public bool isLocked;
    public bool isActivateDialogue;

    [SerializeField]
    private BaseDialogue dialogue;

    private Door door => GetComponent<Door>();

    private void OnEnable()
    {
        if (!isActivateDialogue)
        {
            dialogue.enabled = false;
        }
        if (!isLocked)
        {
            door.enabled = true;
        }
    }
    private void Start()
    {
        if (isActivateDialogue)
        {
            isLocked = true;

            dialogue.OnDialogueFinished += Unlock;
        }
        if (isLocked)
        {
            door.enabled = false;
        }
    }
    private void OnDestroy()
    {
        dialogue.OnDialogueFinished -= Unlock;
        dialogue.OnDialogueFinished -= SwitchStage;
    }

    private void Update()
    {
        if (door.IsPlayerNear)
        {
            if (Input.GetKeyUp(InputData.interactionKey))
            {
                if (!isLocked && !isActivateDialogue)
                {
                    door.enabled = true;
                }
                else if (isLocked)
                {
                    if (Input.GetKeyUp(InputData.interactionKey))
                    {
                        SoundManager.Instance.PlaySound(SoundManager.SoundType.ClosedDoor);

                        OnTriedToOpenClosedDoor?.Invoke();
                    }

                    if (isActivateDialogue)
                    {
                        isActivateDialogue = false;
                        dialogue.enabled = true;
                    }
                }
            }
        }
    }

    private void Unlock(DialogueData dialogueData)
    {
        isLocked = false;

        SwitchStage();

        dialogue.OnDialogueFinished -= SwitchStage;
    }

    private void SwitchStage(DialogueData dialogueData)
    {
        SwitchStage();
    }

    private void SwitchStage()
    {
        door.enabled = true;

        if (door.IsFadingRequired)
        {
            door.SwitchStageWithFade();
        }
        else
        {
            door.SwitchStage();
        }
    }
}