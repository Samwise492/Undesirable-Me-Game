using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DoorBehaviour : MonoBehaviour
{
    // lock state
    public bool isLocked;
    // is triggering dialogue
    [SerializeField, HideInInspector] bool isPlayingDialogue;
    [SerializeField, HideInInspector] int dialogueNumber;

    bool isPlayerNear;
    Talking talkingComponent;
    ChangeScenes changeScenesComp;

    void Start()
    {
        talkingComponent = this.GetComponent<Talking>();
        changeScenesComp = this.GetComponent<ChangeScenes>();
        changeScenesComp.enabled = false;
    }

    void Update()
    {
        if (isPlayerNear)
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (isPlayingDialogue)
                {
                    isPlayingDialogue = false;
                    talkingComponent.enabled = true;
                    talkingComponent.PlayDialogue(dialogueNumber);
                    StartCoroutine(CheckForDialogueEnd());
                }
                else if (!isLocked)
                {
                    changeScenesComp.enabled = true;
                }
            }
    }
    // check is player near
    void OnTriggerEnter2D()
    {
        isPlayerNear = true;
    }
    void OnTriggerExit2D()
    {
        isPlayerNear = false;
    }

    IEnumerator CheckForDialogueEnd()
    {
        while (true)
        {
            if (this.GetComponent<Talking>().IsDialogueFinished)
            {
                changeScenesComp.enabled = true;
                changeScenesComp.ChangeScene();
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
    

#if UNITY_EDITOR
    [CustomEditor(typeof(DoorBehaviour))]
    [CanEditMultipleObjects]
    public class DoorBehaviour_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // for other non-HideInInspector fields
    
            DoorBehaviour script = (DoorBehaviour)target;
            
            script.isPlayingDialogue = EditorGUILayout.Toggle("Play Dialogue", script.isPlayingDialogue);
            if (script.isPlayingDialogue)
            {
                script.isPlayingDialogue = true;
                GUILayout.BeginHorizontal();
                GUILayout.Label("Dialogue (№)", GUILayout.Width(120));
                script.dialogueNumber = EditorGUILayout.IntField(script.dialogueNumber, GUILayout.Width(100));
                GUILayout.EndHorizontal();
            }
            else script.isPlayingDialogue = false;
        }
    }
 #endif
}
