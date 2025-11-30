using MySmartHome.Devices;

namespace SmartHomeSystem;

public class SmartHomeController
{
    public event Action<DayTime>? OnDayTimeChanged;
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

    public void ChangeDayTime(DayTime dayTime)
    {
        Console.WriteLine($"Event: Daytime changed to {dayTime}.");
        logger.Log($"Daytime changed to {dayTime}.");
        OnDayTimeChanged?.Invoke(dayTime);
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
