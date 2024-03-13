using System.Collections;
using UnityEngine;
using TMPro;

public class DebugCatcher : MonoBehaviour
{
    [TextAreaAttribute]
    public string output = "";
    public string stack = "";
    public TextMeshProUGUI DebugWindow;

    void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        DebugWindow.text += output + "\n";
    }
}
