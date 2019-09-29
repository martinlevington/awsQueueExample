using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using awsQueuePublish.Models;
using MessageBusModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace awsQueuePublish.Controllers
{
    public class PublishController : Controller
    {
        private readonly IBus bus;
        private readonly ILogger<Startup> logger;

        public PublishController(IBus bus, ILogger<Startup> logger)
        {
            this.bus = bus;
            this.logger = logger;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            logger.LogInformation("Publishing {MessageCount} messages", 10);

            await Task.WhenAll(
            Enumerable.Range(0, 10)
                .Select(i => new BusMessage("Hi from dot net core"))
                .Select(message => bus.Send(message)));

            var vm = new PublishViewModel();
            vm.Msg = "Rebus sent another 10 messages!";


            return View(vm);
        }



    }
}
