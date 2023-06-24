#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(TriggeredActionMaker))]
public class TriggeredActionMakerEditor : Editor
{
    private SerializedProperty actionType;
    private SerializedProperty dialogueTrigger;
    private SerializedProperty choiceMakerTrigger;
    private SerializedProperty triggerChoiceLineIndex;
    private SerializedProperty isTurnOffAfterFinish;

    private void OnEnable()
    {
        actionType = serializedObject.FindProperty("actionType");

        dialogueTrigger = serializedObject.FindProperty("dialogueTrigger");
        isTurnOffAfterFinish = serializedObject.FindProperty("isTurnOffAfterFinish");

        choiceMakerTrigger = serializedObject.FindProperty("choiceMakerTrigger");
        triggerChoiceLineIndex = serializedObject.FindProperty("triggerChoiceLineIndex");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(actionType);

        switch (actionType.enumNames.GetValue(actionType.enumValueIndex).ToString())
        {
            case nameof(TriggeredActionMaker.ActionType.ActivateDialogue):
                dialogueTrigger.objectReferenceValue = EditorGUILayout.ObjectField(dialogueTrigger.displayName, 
                    dialogueTrigger.objectReferenceValue, typeof(Dialogue), true);
                isTurnOffAfterFinish.boolValue = EditorGUILayout.Toggle(isTurnOffAfterFinish.displayName,
                    isTurnOffAfterFinish.boolValue);
                break;
            case nameof(TriggeredActionMaker.ActionType.LoadScene):
                choiceMakerTrigger.objectReferenceValue = EditorGUILayout.ObjectField(choiceMakerTrigger.displayName, 
                    choiceMakerTrigger.objectReferenceValue, typeof(ChoiceMaker), true);
                triggerChoiceLineIndex.intValue = EditorGUILayout.IntField(triggerChoiceLineIndex.displayName, 
                    triggerChoiceLineIndex.intValue);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif