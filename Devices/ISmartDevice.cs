using System;
using System.Collections.Generic;

namespace MySmartHome.Devices;

public interface ISmartDevice
{
    void HandleTemperatureChangedEvent(int temperature);
    void HandleDayTimeChangedEvent(string dayTime);
    void HandleMotionDetectedEvent();
    void Configure(Dictionary<string, object> settings);
    void ExecuteCommand(string command);
}
