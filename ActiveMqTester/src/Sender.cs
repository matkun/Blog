using Apache.NMS;
using Apache.NMS.ActiveMQ;

using Microsoft.Extensions.Configuration;

using System;

namespace ActiveMqTester
{
    internal class Sender
    {
        public static void Send(IConfigurationSection section)
        {
            (ConnectionFactory factory, string queue, string topic, string username, string password, MsgDeliveryMode deliveryMode, AcknowledgementMode acknowledgementMode) = Settings.GetValues(section);

            using IConnection connection = factory.CreateConnection(username, password);
            connection.Start();

            using ISession session = connection.CreateSession(acknowledgementMode);
            using IDestination destination = GetDestination(session, queue, topic);
            using IMessageProducer producer = session.CreateProducer(destination);

            producer.DeliveryMode = deliveryMode;

            string message;
            while (true)
            {
                Console.Write("\r\nMessage to send (ENTER to quit): ");
                message = Console.ReadLine();
                if (message == string.Empty)
                {
                    break;
                }

                ITextMessage textMessage = session.CreateTextMessage(message);
                producer.Send(textMessage);

                Console.WriteLine($"Message sent {DateTime.Now:HH:mm:ss}");
            }
        }

        private static IDestination GetDestination(ISession session, string queue, string topic) =>
            !string.IsNullOrEmpty(queue) ? session.GetQueue(queue) : session.GetTopic(topic);
    }
}
