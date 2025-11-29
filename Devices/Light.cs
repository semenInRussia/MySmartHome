using System;
using System.Collections.Generic;

namespace MySmartHome.Devices;

public class Light : ISmartDevice
{
    private bool isOn;

    public void HandleEvent(string eventType, object eventData)
    {
        // Implement handling "DayTimeChanged" event:
        // Turn on light in the morning, turn off light at night.
    }

    public void Configure(Dictionary<string, object> settings)
    {
        // Implement configuring light parameters (e.g., brightness).
    }

    public void ExecuteCommand(string command)
    {
        // Implement manual control of the light (turn on/off).
    }
}
