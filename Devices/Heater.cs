namespace MySmartHome.Devices;

public class Heater : ISmartDevice
{
    private int minTemperature = 10;
    private bool isOn;

    public void HandleEvent(string eventType, object eventData)
    {
        if (eventType == "TemperatureChanged")
        {
            int temperature = (int)eventData;
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
    }

    public void Configure(Dictionary<string, object> settings)
    {
        if (settings.ContainsKey("MinTemperature"))
            minTemperature = (int)settings["MinTemperature"];

        Console.WriteLine($"Heater configured: Min={minTemperature}°C.");
    }

    public void ExecuteCommand(string command)
    {
        if (command == "On")
        {
            isOn = true;
            Console.WriteLine("Heater manually turned on.");
        }
        else if (command == "Off")
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
