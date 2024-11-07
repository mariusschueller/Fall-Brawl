using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugConsole : MonoBehaviour
{
    public Text consoleText; // Reference to the Text UI object where logs will be displayed.
    private Queue<string> logQueue = new Queue<string>(); // A queue to store log messages
    public int maxLines = 15; // The maximum number of lines to display

    void OnEnable()
    {
        // Register the callback to capture log messages
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        // Unregister the callback
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Add the new log to the queue
        logQueue.Enqueue(logString);

        // If the queue exceeds the maximum number of lines, remove the oldest one
        if (logQueue.Count > maxLines)
        {
            logQueue.Dequeue();
        }

        // Display the updated log in the UI Text component
        consoleText.text = string.Join("\n", logQueue.ToArray());
    }
}