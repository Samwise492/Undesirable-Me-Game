using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerUIDDatabase))]
public class TriggerUIDDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TriggerUIDDatabase database = (TriggerUIDDatabase)target;

        if (GUILayout.Button("Validate GUID Entries"))
        {
            database.ValidateGUIDEntries();
        }
    }
}