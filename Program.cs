using MySmartHome.Devices;

namespace SmartHomeSystem;

class Program
{
    static void Main(string[] args)
    {
        SmartHomeController controller = new();

        // Create devices
        Light light = new();
        AirConditioner airConditioner = new();
        Heater heater = new();

        // Register devices and subscribe them to events
        controller.RegisterSubscribe(light);
        controller.RegisterSubscribe(airConditioner);
        controller.RegisterSubscribe(heater);

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
            Console.WriteLine("Menu:\n1. Trigger Event\n2. Control Device\n3. Show Event Log\n4. Exit");
            string choice = Console.ReadLine()!;

            if (choice == "1")
            {
                Console.WriteLine("Select event:\n1. Change Daytime\n2. Change Temperature\n3. Detect Motion");
                string eventChoice = Console.ReadLine()!;
                switch (eventChoice)
                {
                    case "1":
                        var dayTime = ReadDayTime();
                        controller.ChangeDayTime(dayTime);
                        break;
                    case "2":
                        Console.Write("Enter temperature: ");
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
                Console.Write("Enter device name: ");
                string deviceName = Console.ReadLine()!;
                Console.Write("Enter command (On/Off): ");
                string command = Console.ReadLine()!;
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

    static DayTime ReadDayTime()
    {
        Console.Write("Enter daytime (Morning/Night): ");

        while (true)
        {
            string input = Console.ReadLine()!;
            if (input == "Morning") return DayTime.Morning;
            if (input == "Night") return DayTime.Morning;
            Console.WriteLine("Sorry, try again (Morning/Night).");
        }
    }
}
