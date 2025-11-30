using SmartHomeSystem;

namespace MySmartHome.Devices;

public class AirConditioner : ISmartDevice
{
    private int minTemperature = 18;
    private int maxTemperature = 25;
    private bool isOn;

    public void HandleTemperatureChangedEvent(int temperature)
    {
        if (temperature > maxTemperature && !isOn)
        {
            isOn = true;
            Console.WriteLine("Air Conditioner turned on (High Temperature).");
        }
        else if (temperature < minTemperature && isOn)
        {
            isOn = false;
            Console.WriteLine("Air Conditioner turned off (Low Temperature).");
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

        Console.WriteLine($"Air Conditioner configured: Min={minTemperature}°C, Max={maxTemperature}°C.");
    }

    public void ExecuteCommand(string command)
    {
        if (command == "On")
        {
            isOn = true;
            Console.WriteLine("Air Conditioner manually turned on.");
        }
        else if (command == "Off")
        {
            isOn = false;
            Console.WriteLine("Air Conditioner manually turned off.");
        }
        else
        {
            Console.WriteLine("Invalid command for Air Conditioner.");
        }
    }
}
