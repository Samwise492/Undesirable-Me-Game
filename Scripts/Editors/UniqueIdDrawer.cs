#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
#region LEGACY BACKUP
// Place this file inside Assets/Editor
// [CustomPropertyDrawer(typeof(UniqueIdentifierAttribute))]
// public class UniqueIdentifierDrawer : PropertyDrawer
// {
//     // public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
//     // {
//     //     // Generate a unique ID, defaults to an empty string if nothing has been serialized yet
//     //     if (prop.stringValue == "")
//     //     {
//     //         Guid guid = Guid.NewGuid();
//     //         prop.stringValue = guid.ToString();
//     //     }
//     //
//     //     // Place a label so it can't be edited by accident
//     //     Rect textFieldPosition = position;
//     //     textFieldPosition.height = 16;
//     //     DrawLabelField(textFieldPosition, prop, label);
//     // }
//     //
//     // void DrawLabelField(Rect position, SerializedProperty prop, GUIContent label)
//     // {
//     //     EditorGUI.LabelField(position, label, new GUIContent(prop.stringValue));
//     // }
//     public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
//     {
//         // Ensure a unique ID exists
//         if (string.IsNullOrEmpty(prop.stringValue))
//         {
//             GenerateUniqueID(prop);
//         }
//
//         // Split the position into two parts: one for the label and one for the button
//         Rect textFieldPosition = new Rect(position.x, position.y, position.width - 70, position.height);
//         Rect buttonPosition = new Rect(position.x + position.width - 65, position.y, 60, position.height);
//
//         // Draw the non-editable label field
//         DrawLabelField(textFieldPosition, prop, label);
//
//         // Draw the regenerate button
//         if (GUI.Button(buttonPosition, "Reset"))
//         {
//             GenerateUniqueID(prop);
//         }
//     }
//
//     void DrawLabelField(Rect position, SerializedProperty prop, GUIContent label)
//     {
//         EditorGUI.LabelField(position, label, new GUIContent(prop.stringValue));
//     }
//
//     void GenerateUniqueID(SerializedProperty prop)
//     {
//         Guid guid = Guid.NewGuid();
//         prop.stringValue = guid.ToString();
//
//         // Mark the property as dirty to ensure it saves the updated value
//         prop.serializedObject.ApplyModifiedProperties();
//     }
// }
#endregion
// Place this file inside Assets/Editor
[CustomPropertyDrawer(typeof(UniqueIdentifierAttribute))]
public class UniqueIdentifierDrawer : PropertyDrawer
{
    private static TriggerUIDDatabase uidDatabase;

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        LoadTriggerUIDDatabase();

        // Ensure the property has a valid GUID
        if (string.IsNullOrEmpty(prop.stringValue))
        {
            GenerateAndStoreGUID(prop);
        }
        
        // Fetch the database
        var database = AssetDatabase.LoadAssetAtPath<TriggerUIDDatabase>("Assets/TriggerUIDDatabase.asset");
        bool isInDatabase = IsInDatabase(prop.stringValue, database, out string sceneName, out string objectName);

        // Calculate button positions (divide the width by 3)
        float buttonWidth = (position.width - 4) / 3; // Subtract spacing for better fit
        float buttonY = position.y + EditorGUIUtility.singleLineHeight + 4;
        float lineY = buttonY + EditorGUIUtility.singleLineHeight + 4;

        CreateLabel(position, prop, label);
        CreateButtons(position, prop, buttonY, buttonWidth);
        CreateDivider(position, lineY);
        
        CreateValidationWarning(position, lineY, isInDatabase, objectName, sceneName);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Increase height to make room for the buttons
        return EditorGUIUtility.singleLineHeight * 4 + 16; // Label + Buttons + Spacing
    }
    
    private static void CreateValidationWarning(Rect position, float lineY, bool isInDatabase, string objectName,
        string sceneName)
    {
        // Draw status message
        float statusY = lineY + 6;
        string statusMessage = isInDatabase 
            ? $"Found in database: {objectName} (Scene: {sceneName})"
            : "Not found in database";
        GUIStyle statusStyle = new GUIStyle(EditorStyles.label)
        {
            fontStyle = isInDatabase ? FontStyle.Bold : FontStyle.Normal,
            normal = { textColor = isInDatabase ? Color.green : Color.red }
        };
        EditorGUI.LabelField(new Rect(position.x, statusY, position.width, EditorGUIUtility.singleLineHeight), statusMessage, statusStyle);
    }

    private void CreateLabel(Rect position, SerializedProperty prop, GUIContent label)
    {
        // Draw the GUID Label
        Rect textFieldPosition = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(textFieldPosition, label, new GUIContent(prop.stringValue));
    }

    private void CreateButtons(Rect position, SerializedProperty prop, float buttonY, float buttonWidth)
    {
        Rect resetButtonPosition = new Rect(position.x, buttonY, buttonWidth, EditorGUIUtility.singleLineHeight);
        Rect addToDatabaseButtonPosition = new Rect(position.x + buttonWidth + 2, buttonY, buttonWidth, EditorGUIUtility.singleLineHeight);
        Rect copyButtonPosition = new Rect(position.x + 2 * (buttonWidth + 2), buttonY, buttonWidth, EditorGUIUtility.singleLineHeight);

        // Draw the "Reset" button
        if (GUI.Button(resetButtonPosition, "Reset UID"))
        {
            GenerateAndStoreGUID(prop);
        }

        // Draw the "Add to Database" button
        if (GUI.Button(addToDatabaseButtonPosition, "Add to UID Database"))
        {
            AddToDatabase(prop);
        }

        // Draw the third button
        if (GUI.Button(copyButtonPosition, "Copy UID"))
        {
            EditorGUIUtility.systemCopyBuffer = prop.stringValue;
        }
    }

    private void CreateDivider(Rect position, float lineY)
    {
        // Draw a divider line beneath the buttons
        Rect lineRect = new Rect(position.x, lineY, position.width, 1);
        EditorGUI.DrawRect(lineRect, new Color(0.5f, 0.5f, 0.5f, 1)); // Gray line
    }
    
    private bool IsInDatabase(string guid, TriggerUIDDatabase database, out string sceneName, out string objectName)
    {
        sceneName = string.Empty;
        objectName = string.Empty;
        if (database == null || string.IsNullOrEmpty(guid))
        {
            return false;
        }

        foreach (var entry in database.entries)
        {
            if (entry.guid == guid)
            {
                sceneName = entry.sceneName;
                objectName = entry.gameObjectName;
                return true;
            }
        }
        return false;
    }
    
    private void AddToDatabase(SerializedProperty prop)
    {
        // Load the GUIDDatabase asset
        var database = AssetDatabase.LoadAssetAtPath<TriggerUIDDatabase>("Assets/TriggerUIDDatabase.asset");
        if (database == null)
        {
            Debug.LogError("GUIDDatabase not found! Please create a GUIDDatabase asset in 'Assets/TriggerUIDDatabase.asset'.");
            return;
        }

        // Get the GameObject and its scene
        var targetObject = prop.serializedObject.targetObject as MonoBehaviour;
        if (targetObject == null)
        {
            Debug.LogError("Target object is not a MonoBehaviour. Cannot add to database.");
            return;
        }

        string sceneName = targetObject.gameObject.scene.name;
        string gameObjectName = targetObject.gameObject.name;
        string guid = prop.stringValue;

        // Add entry to the database
        if (database.GUIDExists(guid))
        {
            Debug.LogWarning($"GUID '{guid}' already exists in the database.");
        }
        else
        {
            database.AddGUID(guid, gameObjectName, sceneName);
            EditorUtility.SetDirty(database); // Mark the database as dirty to save changes
            AssetDatabase.SaveAssets(); // Save the changes to the database
            Debug.Log($"Added GUID '{guid}' for GameObject '{gameObjectName}' in scene '{sceneName}' to the database.");
        }
    }
    
    private void LoadTriggerUIDDatabase()
    {
        if (uidDatabase == null)
        {
            uidDatabase = AssetDatabase.LoadAssetAtPath<TriggerUIDDatabase>("Assets/TriggerUIDDatabase.asset");

            if (uidDatabase == null)
            {
                uidDatabase = ScriptableObject.CreateInstance<TriggerUIDDatabase>();
                AssetDatabase.CreateAsset(uidDatabase, "Assets/TriggerUIDDatabase.asset");
                AssetDatabase.SaveAssets();
                Debug.Log("Created new GUID Database at 'Assets/TriggerUIDDatabase.asset'");
            }
        }
    }

    private void GenerateAndStoreGUID(SerializedProperty prop)
    {
        string newGUID;

        do
        {
            newGUID = Guid.NewGuid().ToString();
        }
        while (uidDatabase.GUIDExists(newGUID)); // Ensure uniqueness

        // Add the new GUID with GameObject and Scene details
        GameObject targetObject = (prop.serializedObject.targetObject as Component)?.gameObject;
        string objectName = targetObject != null ? targetObject.name : "Unknown";
        string sceneName = targetObject != null ? targetObject.scene.name : "Unsaved Scene";

        uidDatabase.AddGUID(newGUID, objectName, sceneName);
        EditorUtility.SetDirty(uidDatabase);

        prop.stringValue = newGUID;
        prop.serializedObject.ApplyModifiedProperties();
    }
}
#endif