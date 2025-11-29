using System;
using System.Collections.Generic;

namespace SmartHomeSystem;

public class EventLogger
{
    private readonly List<string> log = [];

    public void Log(string message)
    {
        log.Add($"MySmartHome:{message}");
    }

    public void ShowLog()
    {
        foreach (var line in log)
        {
            Console.WriteLine($"{line}\n");
        }
    }
}
