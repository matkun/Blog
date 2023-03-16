using Apache.NMS;
using Apache.NMS.ActiveMQ;

using Microsoft.Extensions.Configuration;

using System;

namespace ActiveMqTester
{
    internal class Settings
    {
        public static (ConnectionFactory factory, string queue, string topic, string username, string password, MsgDeliveryMode deliveryMode, AcknowledgementMode acknowledgementMode) GetValues(IConfigurationSection section)
        {
            string hostName = section.GetSection("HostName").Value;
            string queue = section.GetSection("Queue").Value;
            string topic = section.GetSection("Topic").Value;
            int port = int.Parse(section.GetSection("Port").Value);
            string username = section.GetSection("Username").Value;
            string password = section.GetSection("Password").Value;
            var delMode = (MsgDeliveryMode)int.Parse(section.GetSection("MsgDeliveryMode").Value);
            var ackMode = (AcknowledgementMode)int.Parse(section.GetSection("AcknowledgementMode").Value);
            string brokerAddress = $"activemq:tcp://{hostName}:{port}";

            Console.WriteLine("\r\nUsing the following settings from appsettings.json:");
            Console.WriteLine($"HostName:     {hostName}");
            Console.WriteLine($"Port:         {port}");
            Console.WriteLine($"Username:     {username}");
            Console.WriteLine($"Password:     Not shown");
            Console.WriteLine($"Queue:        {queue}");
            Console.WriteLine($"Topic:        {topic}");
            Console.WriteLine($"Del.Mode:     {delMode}");
            Console.WriteLine($"Ack.Mode:     {ackMode}");
            Console.WriteLine($"Broker Url:   {brokerAddress}");

            var brokerUri = new Uri(brokerAddress);
            var factory = new ConnectionFactory(brokerUri);

            return (factory, queue, topic, username, password, delMode, ackMode);
        }
    }
}
