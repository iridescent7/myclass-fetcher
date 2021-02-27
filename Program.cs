using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyClass
{
    class Program
    {
        private static string _cookiePath = Path.Combine(AppContext.BaseDirectory, "cookie.txt");
        private static string _scheduleJsonPath = Path.Combine(AppContext.BaseDirectory, "schedule.json");

        private static MyClassClient client = new MyClassClient();

        static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                password.Append(keyInfo.KeyChar);
                Console.Write('*');
            }
            while (keyInfo.Key != ConsoleKey.Enter);

            password.Length--;
            return password.ToString();
        }

        static async Task Main(string[] args)
        {
            string username, password;

            for (;;)
            {
                Console.Write("username: ");
                username = Console.ReadLine();

                Console.Write("password: ");
                password = ReadPassword();

                bool loginSuccess = await client.Login(username, password);

                if (!loginSuccess)
                    Console.WriteLine("Login failed.\n");
                else
                    break;
            }

            Console.WriteLine("Logged in successfully.");
            Console.WriteLine("Getting vicon schedule...");

            var schedule = await client.GetViconSchedule();

            if (schedule.Count > 0)
            {
                Console.WriteLine("Writing vicon schedule to schedule.json");

                using FileStream scheduleOut = File.Create(_scheduleJsonPath);
                await JsonSerializer.SerializeAsync(scheduleOut, schedule, new JsonSerializerOptions() { WriteIndented = true });

                Console.WriteLine("Finished.");
            }
            else
            {
                Console.WriteLine("Empty / invalid response received.");
            }
        }
    }
}
