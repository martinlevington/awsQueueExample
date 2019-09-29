using System;
using Amazon;
using awsConsoleQueueSubscribe.MessageBus;
using MessageBusModels;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Logging;
using Rebus.Routing.TypeBased;
using Serilog;

namespace awsConsoleQueueSubscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                Console.WriteLine("Starting App");

                var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

                // might even make it global default
                Log.Logger = logger;

                activator.Register(() => new BusMessageHandler());
                var msg = new BusMessage();

                Configure.With(activator)
                .Logging(l => l.Serilog(logger))
                .Transport(t => t.UseAmazonSQS("AKIA5KWE5Z3FX65JN3YF", "d2e5ElbBqdz7wIEANYaByl8zbNDv1mKOTSquBYmW", RegionEndpoint.EUWest2, "rebusQueue"))
                .Routing(r => r.TypeBased().Map<BusMessage>("rebusQueue"))

                .Start();
           
                Console.WriteLine("This is Subscriber 1");
                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
                Console.WriteLine("Quitting...");
            }
        }
    }
}
