using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace RabbitMqTester
{
    class Program
    {
        private const string _help = "Usage:\r\nRabbitMqTester.exe sender (the app will act as a sender).\r\nRabbitMqTester.exe reciever (the app will act as a receiver).";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(_help);
                return;
            }

            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
               .AddJsonFile("appsettings.json", false)
               .Build();
            IConfigurationSection section = configuration.GetSection("TesterSettings");

            Console.WriteLine($"\r\nRabbitMQ tester.");

            switch (args[0])
            {
                case "sender":
                    Sender.Send(section);
                    break;
                case "receiver":
                    Receiver.Receive(section);
                    break;
                default:
                    Console.WriteLine(_help);
                    break;
            }
            Console.WriteLine($"\r\nExiting ...");
        }
    }
}
