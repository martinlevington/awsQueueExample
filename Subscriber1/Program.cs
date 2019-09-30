using System;
using System.IO;
using Amazon;
using MessageBusModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Serilog;

namespace Subscriber1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                Console.WriteLine("Starting App");

                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddJsonFile($"appsettings.Development.json", optional: true);

                IConfigurationRoot configuration = builder.Build();

                var logger = new LoggerConfiguration()
                      .WriteTo.Console()
                     .CreateLogger();


                // might even make it global default
                Log.Logger = logger;

                activator.Register(() => new BusMessageHandler());
                var msg = new SubscribeMessage();

                var accessKeyId = configuration.GetValue<string>("AWS:AccessKeyId");
                var secretAccessKey = configuration.GetValue<string>("AWS:SecretAccessKey");

                Configure.With(activator)
                .Logging(l => l.Serilog(logger))
                .Transport(t => t.UseAmazonSQS(accessKeyId, secretAccessKey, RegionEndpoint.EUWest2, "Subscriber1"))
                .Routing(r => r.TypeBased().Map<SubscribeMessage>("publisher"))

                .Start();

                activator.Bus.Subscribe<SubscribeMessage>().Wait();

                Console.WriteLine("This is Subscriber 1");
                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
                Console.WriteLine("Quitting...");
            }
        }
    }
}
