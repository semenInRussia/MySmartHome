using System;
using System.Collections.Generic;

namespace SmartHomeSystem;

public class EventLogger
{
    private readonly List<string> log = [];

    public void Log(string message)
    {
        var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        log.Add($"{d}:MySmartHome:{message}");
    }

    public void LogWriteLine(string message)
    {
        Log(message);
        Console.WriteLine(message);
    }

    public void LogWrite(string message)
    {
        Log(message);
        Console.WriteLine(message);
    }

    public void ShowLog()
    {
        foreach (var line in log)
        {
            Console.WriteLine($"{line}\n");
        }
    }
}
