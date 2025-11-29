using System;
using System.Collections.Generic;
using MySmartHome.Devices;

namespace SmartHomeSystem;

public class SmartHomeController
{
    public event Action<string>? OnDayTimeChanged;
    public event Action<int>? OnTemperatureChanged;
    public event Action? OnMotionDetected;

    private readonly List<ISmartDevice> devices = [];
    private readonly EventLogger logger = new();

    public void RegisterDevice(ISmartDevice device)
    {
        // Implement adding a device to the devices list.
    }

    public void ChangeDayTime(string timeOfDay)
    {
        Console.WriteLine($"Event: Daytime changed to {timeOfDay}.");
        logger.Log($"Daytime changed to {timeOfDay}.");
        OnDayTimeChanged?.Invoke(timeOfDay);
    }

    public void ChangeTemperature(int temperature)
    {
        // Implement triggering the OnTemperatureChanged event and logging the event.
    }

    public void DetectMotion()
    {
        // Implement triggering the OnMotionDetected event and logging the event.
    }

    public void TriggerDevice(string deviceName, string command)
    {
        // Implement finding the device by name, calling ExecuteCommand, and logging.
    }

    public void ShowLog()
    {
        // Implement showing the event log via logger.
    }
}
