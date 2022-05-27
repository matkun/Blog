using EPiServer.Events;
using System;
using System.ServiceModel;

namespace RemoteEventsTester
{
    class Program
    {
        private static void Main(string[] args)
        {
            Initializer.InitializeContainer();

            if (args.Length > 0 && args[0].Length > 0 && args[0].StartsWith("send"))
            {
                Send();
                return;
            }
            Receive();
        }

        private static void Receive()
        {
            using (var serviceHost = new ServiceHost(typeof(RemoteEventListener)))
            {
                serviceHost.Open();
                Console.WriteLine("Listening. Press any key to exit.");
                _ = Console.ReadLine();
                serviceHost.Close();
            }
        }

        private static void Send()
        {
            Console.WriteLine("Ready to send. Enter a message and press ENTER or only ENTER to exit.");

            var raiserId = Guid.NewGuid();
            string text;
            int sequenceNumber = 0;

            Console.WriteLine($"Raiser id: {raiserId}");

            using (var proxy = new RemoteEventProxy("RemoteEventServiceClientEndPoint"))
            {
                while ((text = Console.ReadLine()).Length > 0)
                {
                    sequenceNumber++;
                    try
                    {
                        var message = new EventMessage
                        {
                            ApplicationName = "Remote events test application",
                            EventId = Guid.NewGuid(),
                            RaiserId = raiserId,
                            SequenceNumber = sequenceNumber,
                            ServerName = Environment.MachineName,
                            SiteId = "RemoteEventsTestApp",
                            Parameter = text
                        };

                        proxy.Interface.RaiseEvent(message);
                        Console.WriteLine($"{message.SequenceNumber}. '{text}' successfully transmitted (id: {message.EventId}).");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}
