using SmartHomeSystem;

namespace MySmartHome.Devices;

public class Heater : ISmartDevice
{
    string ISmartDevice.Name => "Heater";

    private int minTemperature = 10;
    private bool isOn;

    public void HandleTemperatureChangedEvent(int temperature)
    {
        if (temperature >= minTemperature && isOn)
        {
            isOn = false;
            Console.WriteLine("Heater turned off (High Temperature).");
        }
        else if (temperature < minTemperature && isOn)
        {
            isOn = true;
            Console.WriteLine("Heater turned on (Low Temperature).");
        }
    }

    public void HandleDayTimeChangedEvent(DayTime dayTime) { }
    public void HandleMotionDetectedEvent() { }

    public void Configure(Dictionary<string, object> settings)
    {
        if (settings.ContainsKey("MinTemperature"))
            minTemperature = (int)settings["MinTemperature"];

        Console.WriteLine($"Heater configured: Min={minTemperature}°C.");
    }

    public void ExecuteCommand(Command command)
    {
        if (command == Command.On)
        {
            isOn = true;
            Console.WriteLine("Heater manually turned on.");
        }
        else if (command == Command.Off)
        {
            isOn = false;
            Console.WriteLine("Heater manually turned off.");
        }
        else
        {
            Console.WriteLine("Invalid command for Heater.");
        }
    }
}
