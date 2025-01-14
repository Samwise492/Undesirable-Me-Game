using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LogMenu : MonoBehaviour
{
    [SerializeField]
    private Transform logRowNest;
    [SerializeField]
    private LogRow logRowPrefab;
    [SerializeField]
    private GameObject dividerPrefab;

    public void CreateLogs()
    {
        ClearLogs();

        StringBuilder sb = new();

        foreach (KeyValuePair<int, string[]> log in LogDataManager.Instance.GetLogs())
        {
            sb.Clear();

            LogRow row = Instantiate(logRowPrefab, logRowNest);

            for (int i = 0; i < log.Value.Length; i++)
            {
                sb.Append(DialogueParser.FormatDialogue(log.Value[i]) + '\n');
            }

            row.text.text = sb.ToString();

            Instantiate(dividerPrefab, logRowNest);
        }
    }

    public void ClearLogs()
    {
        foreach (Transform child in logRowNest)
        {
            Destroy(child.gameObject);
        }
    }
}