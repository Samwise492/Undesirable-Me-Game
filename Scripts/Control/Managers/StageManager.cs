using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public StageData[] data => FindObjectsOfType<StageData>();

    private void Awake()
    {
        Instance = this;
    }

    public StageData GetCurrent()
    {
        return data.Where(x => x.IsActive()).First();
    }
}
