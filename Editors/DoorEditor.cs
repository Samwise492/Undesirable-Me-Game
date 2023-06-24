#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
    private DoorBehaviourType doorBehaviourType;

    private SerializedProperty sceneToLoadName;
    private SerializedProperty stageToOff, stageToOn;
    private SerializedProperty teleportPosition;
    private SerializedProperty transitionSound;

    private void OnEnable()
    {
        sceneToLoadName = serializedObject.FindProperty("sceneToLoadName");
        stageToOff = serializedObject.FindProperty("stageToOff");
        stageToOn = serializedObject.FindProperty("stageToOn");
        teleportPosition = serializedObject.FindProperty("teleportPosition");
        transitionSound = serializedObject.FindProperty("transitionSound");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        doorBehaviourType = (DoorBehaviourType)EditorGUILayout.EnumPopup("Behaviour Type", doorBehaviourType);

        switch (doorBehaviourType)
        {
            case DoorBehaviourType.Normal:
                stageToOff.objectReferenceValue = EditorGUILayout.ObjectField(stageToOff.displayName, stageToOff.objectReferenceValue, 
                    typeof(GameObject), true);
                stageToOn.objectReferenceValue = EditorGUILayout.ObjectField(stageToOn.displayName, stageToOn.objectReferenceValue, 
                    typeof(GameObject), true);
                teleportPosition.objectReferenceValue = EditorGUILayout.ObjectField(teleportPosition.displayName, 
                    teleportPosition.objectReferenceValue, typeof(Transform), true);
                EditorGUILayout.PropertyField(transitionSound);
                break;
            case DoorBehaviourType.SceneLoader:
                sceneToLoadName.stringValue = EditorGUILayout.TextField(sceneToLoadName.displayName, sceneToLoadName.stringValue);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    public enum DoorBehaviourType
    {
        Normal,
        SceneLoader
    }
}
#endif