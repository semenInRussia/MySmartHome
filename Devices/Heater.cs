using System;
using System.Collections.Generic;

namespace MySmartHome.Devices;

public class Heater : ISmartDevice
{
    private int minTemperature = 10;
    private bool isOn;

    public void HandleEvent(string eventType, object eventData)
    {
        // Implement handling "TemperatureChanged" event:
        // Turn on heater if temperature is below minTemperature.
        // Turn off heater if temperature is above or equal to minTemperature.
    }

    public void Configure(Dictionary<string, object> settings)
    {
        // Implement configuring the minimum temperature for turning on the heater.
    }

    public void ExecuteCommand(string command)
    {
        // Implement manual control of the heater (turn on/off).
    }
}
