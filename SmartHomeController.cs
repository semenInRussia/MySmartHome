using MySmartHome.Devices;

namespace SmartHomeSystem;

public class SmartHomeController(EventLogger _logger)
{
    public event Action<DayTime>? OnDayTimeChanged;
    public event Action<int>? OnTemperatureChanged;
    public event Action? OnMotionDetected;

    private readonly List<ISmartDevice> devices = [];
    private readonly EventLogger logger = _logger;

    public void Register(ISmartDevice device)
    {
        logger.LogWriteLine($"a device registered: {device}");
        devices.Add(device);
    }

    public void Unregister(ISmartDevice device)
    {
        logger.LogWriteLine($"a device unregistered: {device}");
        devices.Remove(device);
    }

    public void Subscribe(ISmartDevice device)
    {
        logger.LogWriteLine($"a device subscripted to all events: {device}");
        OnDayTimeChanged += device.HandleDayTimeChangedEvent;
        OnTemperatureChanged += device.HandleTemperatureChangedEvent;
        OnMotionDetected += device.HandleMotionDetectedEvent;
    }

    public void Unsubscribe(ISmartDevice device)
    {
        logger.LogWriteLine($"a device unsubscripted to all events: {device}");
        OnDayTimeChanged -= device.HandleDayTimeChangedEvent;
        OnTemperatureChanged -= device.HandleTemperatureChangedEvent;
        OnMotionDetected -= device.HandleMotionDetectedEvent;
    }

    public void ChangeDayTime(DayTime dayTime)
    {
        logger.LogWriteLine($"Daytime changed to {dayTime}.");
        Invoke(OnDayTimeChanged, dayTime);
    }

    public void ChangeTemperature(int temperature)
    {
        logger.LogWriteLine($"Temperature changed to {temperature}.");
        Invoke(OnTemperatureChanged, temperature);
    }

    public void DetectMotion()
    {
        logger.LogWriteLine($"A motion detected");
        Invoke(OnMotionDetected);
    }

    public void TriggerDevice(string deviceName, Command command)
    {
        var dev = devices.FirstOrDefault(d => d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));
        logger.LogWriteLine($"trigger a command ({command}) on device ({deviceName})");
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
