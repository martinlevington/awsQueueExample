using System;
namespace MessageBusModels
{
    public class BusMessage
    {

        public BusMessage()
        {
            Id = Guid.NewGuid();
        }

        public BusMessage(string msg)
        {
            Id = Guid.NewGuid();
            Msg = msg;
        }

        public Guid Id { get; }
        public string Msg { get; set; }

        public override string ToString()
        {
            return $"Message : {Id}";
        }
    }
}
