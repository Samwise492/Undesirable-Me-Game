using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public abstract class BasePersistentDataFileManager<T> : MonoBehaviour where T : struct
{
    public static BasePersistentDataFileManager<T> Instance { get; private set; }

    [SerializeField]
    private bool isUseEncryption;

    protected string fileName = "BaseData";

    private const string encryptionCodeWord = "word";
    private const string backupExtension = ".bak";
    private const string fileExtension = ".json";

    protected virtual void Awake()
    {
        Instance = this;
    }

    public virtual bool IsDataExist()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName + fileExtension);

        return File.Exists(fullPath);
    }

    public T LoadData(bool allowRestoreFromBackup = true)
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(Application.persistentDataPath, fileName + fileExtension);
        T loadedData = new();

        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // optionally decrypt the data
                if (isUseEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // deserialize the data from Json back into the C# object
                loadedData = JsonConvert.DeserializeObject<T>(dataToLoad);
            }
            catch (Exception e)
            {
                // since we're calling Load(..) recursively, we need to account for the case where
                // the rollback succeeds, but data is still failing to load for some other reason,
                // which without this check may cause an infinite recursion loop.
                if (allowRestoreFromBackup)
                {
                    Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess)
                    {
                        // try to load again recursively
                        loadedData = LoadData();
                    }
                }
                // if we hit this else block, one possibility is that the backup file is also corrupt
                else
                {
                    Debug.LogError("Error occured when trying to load file at path: "
                        + fullPath + " and backup did not work.\n" + e);
                }
            }
        }
        else
        {
            loadedData = InitFirstSave();
            SaveData(loadedData);
        }

        return loadedData;
    }

    public void SaveData(T data)
    {
        // use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(Application.persistentDataPath, fileName + fileExtension);
        string backupFilePath = fullPath + backupExtension;

        try
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into Json
            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);

            // optionally encrypt the data
            if (isUseEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public void DeletePreviousAndCreateNew()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName + fileExtension);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        T newData = InitFirstSave();
        SaveData(newData);
    }

    protected abstract T InitFirstSave();

    // the below is a simple implementation of XOR encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }

    private bool AttemptRollback(string fullPath)
    {
        bool success = false;
        string backupFilePath = fullPath + backupExtension;
        try
        {
            // if the file exists, attempt to roll back to it by overwriting the original file
            if (File.Exists(backupFilePath))
            {
                File.Copy(backupFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Had to roll back to backup file at: " + backupFilePath);
            }
            // otherwise, we don't yet have a backup file - so there's nothing to roll back to
            else
            {
                throw new Exception("Tried to roll back, but no backup file exists to roll back to.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to roll back to backup file at: "
                + backupFilePath + "\n" + e);
        }

        return success;
    }
}