using System;
using System.Collections.Generic;
using SmartHomeSystem;

namespace MySmartHome.Devices;

public interface ISmartDevice
{
    void HandleTemperatureChangedEvent(int temperature);
    void HandleDayTimeChangedEvent(DayTime dayTime);
    void HandleMotionDetectedEvent();
    void Configure(Dictionary<string, object> settings);
    void ExecuteCommand(string command);
}
