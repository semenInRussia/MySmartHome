namespace MySmartHome.Devices;

using System;
using System.Runtime.Serialization;

public class Light : ISmartDevice
{
    private bool isOn = false;
    private double brightness = 0.0;

    public void HandleEvent(string eventType, object eventData)
    {
        if (eventType == "DayTimeChanged")
        {
            var newDayTime = (string)eventData;
            isOn = newDayTime == "morning";
            var state = isOn ? "on" : "off";
            Console.WriteLine($"Light turned {state}, because time");
        }
    }

    public void Configure(Dictionary<string, object> settings)
    {
        if (settings.TryGetValue("Brightness", out object? val))
            brightness = (double)val;

        Console.WriteLine($"Light configured: Brightness={brightness}");
    }

    public void ExecuteCommand(string command)
    {
        if (command == "On")
        {
            isOn = true;
            Console.WriteLine("Light manually turned on.");
        }
        else if (command == "Of")
        {
            isOn = false;
            Console.WriteLine("Light manually turned off.");
        }
        else
        {
            Console.WriteLine("Invalid command for Light.");
        }
    }
}
