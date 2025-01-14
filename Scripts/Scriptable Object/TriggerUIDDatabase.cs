#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GUIDDatabase", menuName = "Utilities/GUID Database", order = 1)]
public class TriggerUIDDatabase : ScriptableObject
{
    [System.Serializable]
    public class GUIDEntry
    {
        public string guid;
        public string gameObjectName;
        public string sceneName;

        public GUIDEntry(string guid, string gameObjectName, string sceneName)
        {
            this.guid = guid;
            this.gameObjectName = gameObjectName;
            this.sceneName = sceneName;
        }
    }

    public List<GUIDEntry> entries = new List<GUIDEntry>();

    // Add a new GUID entry, ensuring no duplicates
    public bool AddGUID(string guid, string gameObjectName, string sceneName)
    {
        if (!GUIDExists(guid))
        {
            entries.Add(new GUIDEntry(guid, gameObjectName, sceneName));
            return true;
        }
        return false;
    }

    // Check if the GUID exists
    public bool GUIDExists(string guid)
    {
        return entries.Exists(entry => entry.guid == guid);
    }

    public bool IsGUIDDuplicate(string guid)
    {
        return entries.Count(entry => entry.guid == guid) > 1;
    }

    // Optional: Find an entry by GUID
    public GUIDEntry GetEntry(string guid)
    {
        return entries.Find(entry => entry.guid == guid);
    }
    
    public void ValidateGUIDEntries()
    {
        // Record the currently loaded scenes at the start
        var initialScenes = new List<Scene>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            initialScenes.Add(SceneManager.GetSceneAt(i));
        }

        foreach (var entry in entries)
        {
            bool found = false;

            // Attempt to load the scene (if not already loaded)
            Scene scene = SceneManager.GetSceneByName(entry.sceneName);
            if (!scene.isLoaded)
            {
                Debug.Log($"Loading scene '{entry.sceneName}'...");
                scene = EditorSceneManager.OpenScene($"Assets/Undesirable Me/Scenes/Locations/{entry.sceneName}.unity", OpenSceneMode.Additive);
            }

            // Search for the GameObject in the scene
            foreach (GameObject obj in scene.GetRootGameObjects())
            {
                if (SearchGameObjectForGUID(obj, entry))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Debug.LogError($"GUID '{entry.guid}' not found! Expected GameObject '{entry.gameObjectName}' in scene '{entry.sceneName}'.");
            }
        }

        Debug.Log("GUID validation complete. Cleaning up loaded scenes...");

#pragma warning disable CS0618 // Type or member is obsolete
        foreach (var loadedScene in SceneManager.GetAllScenes())
#pragma warning restore CS0618 // Type or member is obsolete
        {
            if (!initialScenes.Contains(loadedScene))
            {
                Debug.Log($"Unloaded scene: {loadedScene.name}");
                EditorSceneManager.CloseScene(loadedScene, true);
            }
        }

        Debug.Log("Scene cleanup complete.");
    }

    private bool SearchGameObjectForGUID(GameObject obj, GUIDEntry entry)
    {
        var myComponent = obj.GetComponent<BaseTrigger>();
        if (myComponent != null && myComponent.Id == entry.guid && obj.name == entry.gameObjectName)
        {
            return true;
        }

        // Search in children
        foreach (Transform child in obj.transform)
        {
            if (SearchGameObjectForGUID(child.gameObject, entry))
                return true;
        }

        return false;
    }
}
#endif