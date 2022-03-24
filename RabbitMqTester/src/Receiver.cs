using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMqTester
{
    internal class Receiver
    {
        public static void Receive(IConfigurationSection section)
        {
            (ConnectionFactory factory, string queue, string exchange, bool durable, bool exclusive, bool autoDelete, bool autoAck) = Settings.GetValues(section);

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue, durable, exclusive, autoDelete, arguments: null);

            Console.WriteLine("\r\n--- Waiting for messages (press any key to quit) ---\r\n");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, args) =>
            {
                byte[] body = args.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received {DateTime.Now:HH:mm:ss}: {message}");
            };
            channel.BasicConsume(queue, autoAck, consumer);

            Console.ReadLine();
        }
    }
}
