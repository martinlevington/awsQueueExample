using System;
namespace MessageBusModels
{
    public class SubscribeMessage
    {

        public SubscribeMessage()
        {
            Id = Guid.NewGuid();
        }

        public SubscribeMessage(string msg)
        {
            Id = Guid.NewGuid();
            Msg = msg;
        }

        public Guid Id { get; }
        public string Msg { get; set; }

        public override string ToString()
        {
            return $"Subscribe Message - Id: {Id} : Msg: {Msg} ";
        }
    }
}
