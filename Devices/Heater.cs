using SmartHomeSystem;

namespace MySmartHome.Devices;

public class Heater(EventLogger _logger) : ISmartDevice
{
    string ISmartDevice.Name => "Heater";

    private int minTemperature = 10;
    private bool isOn;

    readonly private EventLogger logger = _logger;

    public void HandleTemperatureChangedEvent(int temperature)
    {
        if (temperature >= minTemperature && isOn)
        {
            isOn = false;
            logger.LogWriteLine("Heater turned off (High Temperature).");
        }
        else if (temperature < minTemperature && isOn)
        {
            isOn = true;
            logger.LogWriteLine("Heater turned on (Low Temperature).");
        }
    }

    public void HandleDayTimeChangedEvent(DayTime dayTime) { }
    public void HandleMotionDetectedEvent() { }

    public void Configure(Dictionary<string, object> settings)
    {
        if (settings.TryGetValue("MinTemperature", out object? value))
            minTemperature = (int)value;

        logger.LogWriteLine($"Heater configured: Min={minTemperature}°C.");
    }

    public void ExecuteCommand(Command command)
    {
        if (command == Command.On)
        {
            isOn = true;
            logger.LogWriteLine("Heater manually turned on.");
        }
        else if (command == Command.Off)
        {
            isOn = false;
            logger.LogWriteLine("Heater manually turned off.");
        }
        else
        {
            logger.LogWriteLine("Invalid command for Heater.");
        }
    }
}
