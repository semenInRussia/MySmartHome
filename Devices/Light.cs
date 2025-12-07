namespace MySmartHome.Devices;

using System;
using System.Runtime.Serialization;
using SmartHomeSystem;

public class Light(EventLogger _logger) : ISmartDevice
{
    string ISmartDevice.Name => "Light";

    private bool isOn = false;
    private double brightness = 0.0;

    private EventLogger logger = _logger;

    public void HandleDayTimeChangedEvent(DayTime newDayTime)
    {
        isOn = newDayTime == DayTime.Morning;
        var state = isOn ? "on" : "off";
        logger.LogWriteLine($"Light turned {state}, because time");
    }

    public void HandleTemperatureChangedEvent(int temperature) { }
    public void HandleMotionDetectedEvent() { }

    public void Configure(Dictionary<string, object> settings)
    {
        if (settings.TryGetValue("Brightness", out object? val))
            brightness = (double)val;

        logger.LogWriteLine($"Light configured: Brightness={brightness}");
    }

    public void ExecuteCommand(Command command)
    {
        if (command == Command.On)
        {
            isOn = true;
            logger.LogWriteLine("Light manually turned on.");
        }
        else if (command == Command.Off)
        {
            isOn = false;
            logger.LogWriteLine("Light manually turned off.");
        }
        else
        {
            logger.LogWriteLine("Invalid command for Light.");
        }
    }
}
