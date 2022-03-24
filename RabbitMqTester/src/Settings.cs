using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;

namespace RabbitMqTester
{
    internal class Settings
    {
        public static (ConnectionFactory factory, string queue, string exchange, bool durable, bool exclusive, bool autoDelete, bool autoAck) GetValues(IConfigurationSection section)
        {
            string hostName = section.GetSection("HostName").Value;
            string username = section.GetSection("UserName").Value;
            string password = section.GetSection("Password").Value;
            string queue = section.GetSection("Queue").Value;
            string exchange = section.GetSection("Exchange").Value;
            int port = int.Parse(section.GetSection("Port").Value);
            bool durable = bool.Parse(section.GetSection("Durable").Value);
            bool exclusive = bool.Parse(section.GetSection("Exclusive").Value);
            bool autoDelete = bool.Parse(section.GetSection("AutoDelete").Value);
            bool autoAck = bool.Parse(section.GetSection("AutoAck").Value);

            Console.WriteLine("\r\nUsing the following settings from appsettings.json:");
            Console.WriteLine($"HostName:   {hostName}");
            Console.WriteLine($"Port:       {port}");
            Console.WriteLine($"UserName:   {username}");
            Console.WriteLine($"Queue:      {queue}");
            Console.WriteLine($"Exchange:   {exchange}");
            Console.WriteLine($"Durable:    {durable}");
            Console.WriteLine($"Exclusive:  {exclusive}");
            Console.WriteLine($"AutoDelete: {autoDelete}");
            Console.WriteLine($"AutoAck:    {autoAck}");

            var factory = new ConnectionFactory { HostName = hostName, Port = port, UserName = username, Password = password };
            return (factory, queue, exchange, durable, exclusive, autoDelete, autoAck);
        }
    }
}
