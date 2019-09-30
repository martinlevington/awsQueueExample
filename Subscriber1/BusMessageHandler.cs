using System;
using System.Threading.Tasks;
using MessageBusModels;
using Rebus.Handlers;

namespace Subscriber1
{
    public class BusMessageHandler : IHandleMessages<SubscribeMessage> 
    {
        public BusMessageHandler()
        {
        }


        public async Task Handle(SubscribeMessage message)
        {
             Console.WriteLine("Got string: id: {0} msg: {1}", message.Id, message.Msg);

        }

           
        }
    }
