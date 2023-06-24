using UnityEngine;

[RequireComponent(typeof (Door))]
public class SpecificDoorBehaviour : MonoBehaviour
{
    public bool isLocked;
    public bool isActivateDialogue;

    private Dialogue dialogue => GetComponent<Dialogue>();
    private Door door => GetComponent<Door>();

    private bool isPlayerNear;

    private void Start()
    {
        if (isActivateDialogue)
        {
            isLocked = true;
            dialogue.enabled = false;

            dialogue.OnDialogueFinished.AddListener(delegate 
            { 
                isLocked = false;
                door.SwitchStage(); 
                
                dialogue.OnDialogueFinished.RemoveListener(door.SwitchStage);
            });
        }
        if (isLocked)
        {
            door.enabled = false;
        }
    }
    private void OnEnable()
    {
        if (isActivateDialogue == false)
        {
            dialogue.enabled = false;
        }
        if (!isLocked)
        {
            door.enabled = true;
        }
    }
    private void OnDestroy()
    {
        dialogue.OnDialogueFinished.RemoveAllListeners();
    }
    
    private void Update()
    {
        if (isPlayerNear)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (!isLocked && !isActivateDialogue)
                {
                    door.enabled = true;
                }
                else if (isLocked)
                {
                    if (isActivateDialogue)
                    {
                        isActivateDialogue = false;
                        dialogue.enabled = true;

                        dialogue.OnPlayDialogue?.Invoke();
                        
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D() => isPlayerNear = true;
    private void OnTriggerExit2D() => isPlayerNear = false;
}