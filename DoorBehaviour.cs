using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour
{
    public bool isLocked;
    [SerializeField] bool doesActivateDialogue;
    bool isPlayerNear;
    Talking talkingComponent;
    ChangeScenes changeScenesComponent;

    void Start()
    {
        talkingComponent = this.GetComponent<Talking>();
        changeScenesComponent = this.GetComponent<ChangeScenes>();
        changeScenesComponent.enabled = false;
    }

    void Update()
    {
        if (isPlayerNear)
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (!isLocked)
                {
                    changeScenesComponent.enabled = true;
                }
                if (doesActivateDialogue)
                {
                    doesActivateDialogue = false;

                    talkingComponent.enabled = true;
                    talkingComponent.PlayDialogue(talkingComponent.dialogueNumber);
                    StartCoroutine(CheckForDialogueEnd());
                }
            }
    }

    void OnTriggerEnter2D() => isPlayerNear = true;
    void OnTriggerExit2D() => isPlayerNear = false;

    IEnumerator CheckForDialogueEnd()
    {
        while (true)
        {
            if (this.GetComponent<Talking>().IsDialogueFinished)
            {
                changeScenesComponent.enabled = true;
                changeScenesComponent.ChangeScene();
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
