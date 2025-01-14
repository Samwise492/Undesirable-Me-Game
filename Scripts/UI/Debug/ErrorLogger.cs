using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorLogger : MonoBehaviour
{
#if DEVELOPMENT_BUILD
    private List<string> errorMessages = new List<string>();
    private Vector2 scrollPosition;
    private string combinedErrors = "";

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            string errorMessage = $"{logString}\n{stackTrace}\n";
            errorMessages.Add(errorMessage);
            combinedErrors = string.Join("\n", errorMessages);
        }
    }

    private void OnGUI()
    {
        // Panel dimensions
        Rect panelRect = new Rect(10, 10, Screen.width - 20, Screen.height - 20);
        GUI.Box(panelRect, "Error Logger");

        // Scrollable area for errors
        Rect scrollRect = new Rect(20, 40, panelRect.width - 20, panelRect.height - 80);
        scrollPosition = GUI.BeginScrollView(scrollRect, scrollPosition, new Rect(0, 0, scrollRect.width - 20, errorMessages.Count * 20));

        GUI.TextArea(new Rect(0, 0, scrollRect.width - 40, Mathf.Max(20, errorMessages.Count * 20)), combinedErrors);

        GUI.EndScrollView();

        // Copy to Clipboard button
        if (GUI.Button(new Rect(20, panelRect.height - 40, 100, 30), "Copy Text"))
        {
            GUIUtility.systemCopyBuffer = combinedErrors;
        }

        // Clear messages button
        if (GUI.Button(new Rect(130, panelRect.height - 40, 100, 30), "Clear Log"))
        {
            errorMessages.Clear();
            combinedErrors = "";
        }
    }
#endif
}
