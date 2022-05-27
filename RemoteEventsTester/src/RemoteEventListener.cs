using EPiServer.Events;
using EPiServer.Events.ServiceModel;
using System;

namespace RemoteEventsTester
{
    internal class RemoteEventListener : IEventReplication
    {
        private static readonly object _lock;

        static RemoteEventListener()
        {
            _lock = new object();
        }

        public void RaiseEvent(EventMessage message)
        {
            lock (_lock)
            {
                Console.WriteLine("");
                Console.WriteLine("****************** Event Start ********************");
                Console.WriteLine(" Time received   : " + DateTime.Now.ToString());
                Console.WriteLine(" Raiser Id       : " + message.RaiserId.ToString());
                Console.WriteLine(" Site Id         : " + message.SiteId.ToString());
                Console.WriteLine(" Sequence Number : " + message.SequenceNumber);
                Console.WriteLine(" Event Id        : " + message.EventId.ToString());
                Console.WriteLine(" Source server   : " + message.ServerName.ToString());
                Console.WriteLine(" Message         : " + message.Parameter.ToString());
                Console.WriteLine("****************** Event End *********************");
                Console.WriteLine("");
            }
        }
    }
}
