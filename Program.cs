using MySmartHome.Devices;

namespace SmartHomeSystem;

class Program
{
    static void Main(string[] args)
    {
        EventLogger logger = new();
        SmartHomeController controller = new(logger);

        // Create devices
        Light light = new(logger);
        AirConditioner airConditioner = new(logger);
        Heater heater = new(logger);

        // Register devices and subscribe them to events
        controller.Register(light);
        controller.Register(airConditioner);
        controller.Register(heater);

        controller.Subscribe(light);
        controller.Subscribe(airConditioner);
        controller.Subscribe(heater);

        // Subscribe devices to events
        // we don't need in following lines, because register already subscribe on controller events
        // controller.OnDayTimeChanged += time => light.HandleEvent("DayTimeChanged", time);
        // controller.OnTemperatureChanged += temp => airConditioner.HandleEvent("TemperatureChanged", temp);
        // controller.OnTemperatureChanged += temp => heater.HandleEvent("TemperatureChanged", temp);

        // Example of configuring a device
        Dictionary<string, object> acSettings = new()
        {
              { "MinTemperature", 20 },
              { "MaxTemperature", 30 }
          };
        airConditioner.Configure(acSettings);

        // Control menu
        while (true)
        {
            logger.LogWriteLine("Menu:\n1. Trigger Event\n2. Control Device\n3. Show Event Log\n4. Exit");
            string choice = Console.ReadLine()!;

            if (choice == "1")
            {
                logger.LogWriteLine("Select event:\n1. Change Daytime\n2. Change Temperature\n3. Detect Motion");
                string eventChoice = Console.ReadLine()!;
                switch (eventChoice)
                {
                    case "1":
                        var dayTime = ReadDayTime(logger);
                        controller.ChangeDayTime(dayTime);
                        break;
                    case "2":
                        logger.LogWrite("Enter temperature: ");
                        int temp = int.Parse(Console.ReadLine()!);
                        controller.ChangeTemperature(temp);
                        break;
                    case "3":
                        controller.DetectMotion();
                        break;
                }
            }
            else if (choice == "2")
            {
                logger.LogWrite("Enter device name: ");
                string deviceName = Console.ReadLine()!;
                var command = ReadCommand(logger);
                controller.TriggerDevice(deviceName, command);
            }
            else if (choice == "3")
            {
                controller.ShowLog();
            }
            else if (choice == "4")
            {
                break;
            }
        }
    }

    static DayTime ReadDayTime(EventLogger logger)
    {
        while (true)
        {
            logger.LogWrite("Enter daytime (Morning/Night): ");
            string input = Console.ReadLine()!;
            if (input == "Morning") return DayTime.Morning;
            if (input == "Night") return DayTime.Night;
            logger.LogWriteLine("Sorry, try again.");
        }
    }

    static Command ReadCommand(EventLogger logger)
    {
        while (true)
        {
            logger.LogWrite("Enter command (On/Off): ");
            string input = Console.ReadLine()!;
            if (input == "On") return Command.On;
            if (input == "Off") return Command.Off;
            logger.LogWriteLine("Sorry, try again.");
        }
    }
}
