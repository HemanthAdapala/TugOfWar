using UnityEngine;

public static class DebugHelper
{
    public static void LogColor(string message, Color color)
    {
        Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>");
    }
}
