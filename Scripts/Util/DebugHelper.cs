using System;
using Godot;

public static class DebugHelper
{
    public static void DebugPrint(string message)
    {
        string timeStamp = DateTime.Now.ToString("h:mm:ss");
        string stamp = timeStamp + " :: " + message;
        GD.Print(stamp);
    }
}