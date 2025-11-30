using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using MySmartHome.Devices;

namespace SmartHomeSystem;

public class SmartHomeController
{
    public event Action<string>? OnDayTimeChanged;
    public event Action<int>? OnTemperatureChanged;
    public event Action? OnMotionDetected;

    private readonly List<ISmartDevice> devices = [];
    private readonly EventLogger logger = new();

    public void RegisterSubscribe(ISmartDevice device)
    {
        devices.Add(device);
        OnDayTimeChanged += device.HandleDayTimeChangedEvent;
        OnTemperatureChanged += device.HandleTemperatureChangedEvent;
        OnMotionDetected += device.HandleMotionDetectedEvent;
    }

    public void ChangeDayTime(string timeOfDay)
    {
        Console.WriteLine($"Event: Daytime changed to {timeOfDay}.");
        logger.Log($"Daytime changed to {timeOfDay}.");
        OnDayTimeChanged?.Invoke(timeOfDay);
    }

    public void ChangeTemperature(int temperature)
    {
        Console.WriteLine($"Event: Temperature changed to {temperature}.");
        logger.Log($"Temperature changed to {temperature}.");
        OnTemperatureChanged?.Invoke(temperature);
    }

    public void DetectMotion()
    {
        Console.WriteLine($"Event: Motion detected.");
        logger.Log($"A motion detected");
        OnMotionDetected?.Invoke();
    }

    public void TriggerDevice(string deviceName, string command)
    {
        foreach (var dev in devices)
        {
            if (deviceName == dev.GetType().Name)
            {
                dev.ExecuteCommand(command);
            }
        }
    }

    public void ShowLog()
    {
        logger.ShowLog();
    }
}
