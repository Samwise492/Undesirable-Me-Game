#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using static DoorEditor;

[CustomEditor(typeof(SpecificDoorBehaviour))]
public class SpecificDoorBehaviourEditor : Editor
{
    public SerializedProperty isLocked;

    public SerializedProperty isActivateDialogue;
    public SerializedProperty dialogue;

    private void OnEnable()
    {
        isLocked = serializedObject.FindProperty("isLocked");
        isActivateDialogue = serializedObject.FindProperty("isActivateDialogue");
        dialogue = serializedObject.FindProperty("dialogue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        isLocked.boolValue = EditorGUILayout.Toggle(isLocked.displayName,
            isLocked.boolValue);

        isActivateDialogue.boolValue = EditorGUILayout.Toggle(isActivateDialogue.displayName, 
            isActivateDialogue.boolValue);

        if (isActivateDialogue.boolValue == true)
        {
            dialogue.objectReferenceValue = EditorGUILayout.ObjectField(dialogue.displayName, dialogue.objectReferenceValue,
                    typeof(BaseDialogue), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif