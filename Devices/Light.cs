namespace MySmartHome.Devices;

using System;
using System.Runtime.Serialization;
using SmartHomeSystem;

public class Light : ISmartDevice
{
    private bool isOn = false;
    private double brightness = 0.0;

    public void HandleDayTimeChangedEvent(DayTime newDayTime)
    {
        isOn = newDayTime == DayTime.Morning;
        var state = isOn ? "on" : "off";
        Console.WriteLine($"Light turned {state}, because time");
    }

    public void HandleTemperatureChangedEvent(int temperature) { }
    public void HandleMotionDetectedEvent() { }

    public void Configure(Dictionary<string, object> settings)
    {
        if (settings.TryGetValue("Brightness", out object? val))
            brightness = (double)val;

        Console.WriteLine($"Light configured: Brightness={brightness}");
    }

    public void ExecuteCommand(Command command)
    {
        if (command == Command.On)
        {
            isOn = true;
            Console.WriteLine("Light manually turned on.");
        }
        else if (command == Command.Off)
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
