using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

// Script which makes a log file at ""Persistent Data Path" which contains most of the logs unity creates in the console, so you can save debug logs and such.

public class LogHandler : MonoBehaviour
{
    private string logPath;

    void Start()
    {
        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
    }

    void OnEnable()
    {
        logPath = Application.persistentDataPath + "/game_logs.txt";
        Application.logMessageReceived += HandleLog;
        Debug.Log("Log Handler is enabled, writing to: " + logPath);
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Filter out unnecessary Unity internal logs if needed
        if (logString.Contains("Some filter condition")) return;

        string formattedMessage = FormatLogMessage(logString, stackTrace, type);
        using (StreamWriter writer = new StreamWriter(logPath, true))
        {
            writer.WriteLine(formattedMessage);
        }
    }

    private string FormatLogMessage(string message, string stackTrace, LogType type)
    {
        // Formatting the date and time for each log entry
        string logTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        // Including the type of log and message
        string formattedMessage = $"{logTime} [{type}] {message}";

        // Optional: Include stack trace for exceptions or errors
        if (type == LogType.Error || type == LogType.Exception)
        {
            formattedMessage += "\nStack Trace:\n" + stackTrace;
        }

        // Filtering the stack trace to include only the relevant parts
        formattedMessage += "\nFiltered Stack Trace:\n" + FilterStackTrace(stackTrace);

        return formattedMessage;
    }

    private string FilterStackTrace(string stackTrace)
    {
        if (string.IsNullOrEmpty(stackTrace)) return "";

        var lines = stackTrace.Split('\n');
        string filteredTrace = "";
        foreach (var line in lines)
        {
            // Filter to exclude lines containing certain keywords
            if (!line.Contains("UnityEngine") && !line.Contains("System"))
            {
                filteredTrace += line + "\n";
            }
        }
        return filteredTrace.Trim();
    }
}