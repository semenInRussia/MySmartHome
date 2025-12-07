using SmartHomeSystem;

namespace MySmartHome.Devices;

public class AirConditioner(EventLogger _logger) : ISmartDevice
{
    string ISmartDevice.Name => "AirConditioner";

    private int minTemperature = 18;
    private int maxTemperature = 25;
    private bool isOn;

    private readonly EventLogger logger = _logger;

    public void HandleTemperatureChangedEvent(int temperature)
    {
        if (temperature > maxTemperature && !isOn)
        {
            isOn = true;
            logger.LogWriteLine("Air Conditioner turned on (High Temperature).");
        }
        else if (temperature < minTemperature && isOn)
        {
            isOn = false;
            logger.LogWriteLine("Air Conditioner turned off (Low Temperature).");
        }
    }

    public void HandleDayTimeChangedEvent(DayTime dayTime) { }
    public void HandleMotionDetectedEvent() { }

    public void Configure(Dictionary<string, object> settings)
    {
        if (settings.ContainsKey("MinTemperature"))
            minTemperature = (int)settings["MinTemperature"];
        if (settings.ContainsKey("MaxTemperature"))
            maxTemperature = (int)settings["MaxTemperature"];

        logger.LogWriteLine($"Air Conditioner configured: Min={minTemperature}°C, Max={maxTemperature}°C.");
    }

    public void ExecuteCommand(Command command)
    {
        if (command == Command.On)
        {
            isOn = true;
            logger.LogWriteLine("Air Conditioner manually turned on.");
        }
        else if (command == Command.Off)
        {
            isOn = false;
            logger.LogWriteLine("Air Conditioner manually turned off.");
        }
        else
        {
            logger.LogWriteLine("Invalid command for Air Conditioner.");
        }
    }
}
