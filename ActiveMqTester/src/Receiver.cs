using Apache.NMS;
using Apache.NMS.ActiveMQ;

using Microsoft.Extensions.Configuration;

using System;

namespace ActiveMqTester
{
    internal class Receiver
    {
        public static void Receive(IConfigurationSection section)
        {
            (ConnectionFactory factory, string queue, string username, string password, MsgDeliveryMode deliveryMode, AcknowledgementMode acknowledgementMode) = Settings.GetValues(section);


            using IConnection connection = factory.CreateConnection(username, password);
            connection.Start();

            using ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            using IDestination destination = session.GetQueue(queue);
            using IMessageConsumer consumer = session.CreateConsumer(destination);

            Console.WriteLine("\r\n--- Waiting for messages (press any key to quit) ---\r\n");

            consumer.Listener += (IMessage message) =>
            {
                var textMessage = message as ITextMessage;
                Console.WriteLine($"Received {DateTime.Now:HH:mm:ss}: {textMessage.Text}");
            };

            _ = Console.ReadLine();
        }
    }
}
