using System;
using System.Threading.Tasks;
using MessageBusModels;
using Rebus.Handlers;

namespace awsQueueSubscribe.MessageBus
{
    public class BusMessageHandler : IHandleMessages<BusMessage> 
    {
        public BusMessageHandler()
        {
        }


        public async Task Handle(BusMessage message)
        {
             Console.WriteLine("Got string: id: {0} {1}", message.Id, message.Msg);

        }

           
        }
    }
