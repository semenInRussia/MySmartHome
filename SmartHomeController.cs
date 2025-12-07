using MySmartHome.Devices;

namespace SmartHomeSystem;

public class SmartHomeController
{
    public event Action<DayTime>? OnDayTimeChanged;
    public event Action<int>? OnTemperatureChanged;
    public event Action? OnMotionDetected;

    private readonly List<ISmartDevice> devices = [];
    private readonly EventLogger logger = new();

    public void Register(ISmartDevice device)
    {
        devices.Add(device);
    }

    public void Unregister(ISmartDevice device)
    {
        devices.Remove(device);
    }

    public void Subscribe(ISmartDevice device)
    {
        OnDayTimeChanged += device.HandleDayTimeChangedEvent;
        OnTemperatureChanged += device.HandleTemperatureChangedEvent;
        OnMotionDetected += device.HandleMotionDetectedEvent;
    }

    public void Unsubscribe(ISmartDevice device)
    {
        OnDayTimeChanged -= device.HandleDayTimeChangedEvent;
        OnTemperatureChanged -= device.HandleTemperatureChangedEvent;
        OnMotionDetected -= device.HandleMotionDetectedEvent;
    }

    public void ChangeDayTime(DayTime dayTime)
    {
        Console.WriteLine($"Event: Daytime changed to {dayTime}.");
        logger.Log($"Daytime changed to {dayTime}.");
        Invoke(OnDayTimeChanged, dayTime);
    }

    public void ChangeTemperature(int temperature)
    {
        Console.WriteLine($"Event: Temperature changed to {temperature}.");
        logger.Log($"Temperature changed to {temperature}.");
        Invoke(OnTemperatureChanged, temperature);
    }

    public void DetectMotion()
    {
        Console.WriteLine($"Event: Motion detected.");
        logger.Log($"A motion detected");
        Invoke(OnMotionDetected);
    }

    public void TriggerDevice(string deviceName, Command command)
    {
        var dev = devices.FirstOrDefault(d => d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));
        logger.Log($"trigger a command ({command}) on device ({deviceName})");
        dev!.ExecuteCommand(command);
    }

    public void ShowLog()
    {
        logger.ShowLog();
    }

    private void Invoke(Delegate? Ev, params object[] args)
    {
        if (Ev == null) return;
        foreach (var func in Ev.GetInvocationList())
        {
            try
            {
                func?.DynamicInvoke(args);
            }
            catch
            {
                logger.LogWriteLine($"Caught exception when invoke a event: {Ev}");
            }
        }
    }
}
