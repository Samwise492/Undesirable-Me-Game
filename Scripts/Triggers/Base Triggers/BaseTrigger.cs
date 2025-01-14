using NaughtyAttributes;
using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour, ITrigger
{
    public event Action OnTriggerActionMade;

    public event Action<BaseTrigger> OnRequestFade;
    public event Action<float> OnRequestOverrideFadeLength;

    public string Id => id;
    public bool IsActionOnEnd => isActionOnEnd;
    public bool IsActionOnProcess => isActionOnProcess;

    [UniqueIdentifier]
    [SerializeField]
    private string id;

    [SerializeField]
    private bool isFadeBeforeAction;

    [ShowIf("isFadeBeforeAction")]
    [SerializeField]
    protected bool isFadeBeforeSceneLoad;

    [ShowIf("isFadeBeforeAction")]
    [SerializeField]
    private bool isActionOnEnd;

    [ShowIf("isFadeBeforeAction")]
    [SerializeField]
    private bool isActionOnProcess;

    [ShowIf("isFadeBeforeAction")]
    [SerializeField]
    private bool isOverrideFadePause;

    [ShowIf("isOverrideFadePause")]
    [SerializeField]
    private float customFadePause;

    public abstract void TriggerAction();

    #if UNITY_EDITOR
    private void OnValidate()
    {
        ValidateGUID();
    }

    private void ValidateGUID()
    {
        // Load the GUIDDatabase asset using AssetDatabase
        var database = AssetDatabase.LoadAssetAtPath<TriggerUIDDatabase>("Assets/TriggerUIDDatabase.asset");

        if (database == null)
        {
            Debug.LogWarning("GUIDDatabase not found at 'Assets/TriggerUIDDatabase.asset'. Please create the database.");
            return;
        }

        if (!string.IsNullOrEmpty(id) && database.IsGUIDDuplicate(id))
        {
            var entry = database.GetEntry(id);
            Debug.LogError($"Duplicate GUID detected! Existing on '{entry.gameObjectName}' in scene '{entry.sceneName}'. GUID: {id}");
        }
    }
    #endif
    
    private void OnEnable()
    {
        Debug.Log(DebugData.TriggerDebugKey + gameObject.name + $" is {id}. Is it passed: {PlayerSaveLoadProvider.Instance.GetCurrentSave().passedTriggerGuids.Contains(Id)}");
    }

    protected void EndTrigger()
    {
        PlayerSaveStorage.PlayerSave playerSave = PlayerSaveLoadProvider.Instance.GetCurrentSave();
        playerSave.passedTriggerGuids.Add(Id);

        PlayerSaveLoadProvider.Instance.SaveInTemp(playerSave);

        OnTriggerActionMade?.Invoke();
    }

    protected void CheckTriggerBehaviour()
    {
        if (isFadeBeforeAction)
        {
            if (isOverrideFadePause)
            {
                OnRequestOverrideFadeLength?.Invoke(customFadePause);
            }

            OnRequestFade?.Invoke(this);
        }
        else
        {
            TriggerAction();
        }
    }
}