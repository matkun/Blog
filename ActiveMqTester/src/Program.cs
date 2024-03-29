﻿using Microsoft.Extensions.Configuration;

using System;
using System.IO;

namespace ActiveMqTester
{
    internal class Program
    {
        private const string _help = "Usage:\r\nActiveMqTester.exe sender (the app will act as a sender).\r\nActiveMqTester.exe reciever (the app will act as a receiver).\r\n\r\nIf Queue is empty in config Topic will be used instead.";

        private static void Main(string[] args)
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

            Console.WriteLine($"\r\nActiveMQ tester.");

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
