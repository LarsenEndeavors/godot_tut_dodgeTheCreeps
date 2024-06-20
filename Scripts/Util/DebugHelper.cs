using System;
using System.IO;
using Godot;

public static class DebugHelper
{
    private static string logDirectory = "Logs/";
    private static string logFile = "game_log.txt";

    static DebugHelper()
    {
        // Ensure the directory exists
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
    }

    public static void DebugPrint(string message)
    {
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss");
        string logMessage = timeStamp + " :: " + message + System.Environment.NewLine;

        // Write to log file
        File.AppendAllText(logDirectory + logFile, logMessage);

        // Output to console
        GD.Print(logMessage);
    }
}