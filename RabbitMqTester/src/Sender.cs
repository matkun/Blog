using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMqTester
{
    internal class Sender
    {
        public static void Send(IConfigurationSection section)
        {
            (ConnectionFactory factory, string queue, string exchange, bool durable, bool exclusive, bool autoDelete, bool autoAck) = Settings.GetValues(section);

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue, durable, exclusive, autoDelete, arguments: null);

            string message;
            while (true)
            {
                Console.Write("\r\nMessage to send (ENTER to quit): ");
                message = Console.ReadLine();
                if (message == string.Empty)
                {
                    break;
                }

                byte[] body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange, routingKey: queue, basicProperties: null, body);
                Console.WriteLine($"Message sent {DateTime.Now:HH:mm:ss}");
            }
        }
    }
}
