#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
    private SerializedProperty doorType;

    private SerializedProperty sceneToLoadName;
    private SerializedProperty sceneToLoadMeta;

    private SerializedProperty stageToOff, stageToOn;
    private SerializedProperty teleportPosition;
    private SerializedProperty transitionSound;

    private SerializedProperty isFadingRequired;

    private void OnEnable()
    {
        doorType = serializedObject.FindProperty("type");

        sceneToLoadName = serializedObject.FindProperty("sceneToLoadData.sceneToLoadName");
        sceneToLoadMeta = serializedObject.FindProperty("sceneToLoadData.sceneToLoadMeta");

        stageToOff = serializedObject.FindProperty("stageToOff");
        stageToOn = serializedObject.FindProperty("stageToOn");
        teleportPosition = serializedObject.FindProperty("teleportPosition");
        transitionSound = serializedObject.FindProperty("transitionSound");

        isFadingRequired = serializedObject.FindProperty("isFadingRequired");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(doorType);

        isFadingRequired.boolValue = EditorGUILayout.Toggle(isFadingRequired.displayName, isFadingRequired.boolValue);

        switch (doorType.enumValueIndex)
        {
            case 2:
                sceneToLoadName.stringValue = EditorGUILayout.TextField(sceneToLoadName.displayName, sceneToLoadName.stringValue);
                sceneToLoadMeta.stringValue = EditorGUILayout.TextField(sceneToLoadMeta.displayName, sceneToLoadMeta.stringValue);
                EditorGUILayout.PropertyField(transitionSound);
                break;
            default:
                stageToOff.objectReferenceValue = EditorGUILayout.ObjectField(stageToOff.displayName, stageToOff.objectReferenceValue,
                    typeof(GameObject), true);
                stageToOn.objectReferenceValue = EditorGUILayout.ObjectField(stageToOn.displayName, stageToOn.objectReferenceValue,
                    typeof(GameObject), true);
                teleportPosition.objectReferenceValue = EditorGUILayout.ObjectField(teleportPosition.displayName,
                    teleportPosition.objectReferenceValue, typeof(Transform), true);
                EditorGUILayout.PropertyField(transitionSound);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif