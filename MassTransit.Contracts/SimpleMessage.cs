namespace MassTransit.Contracts
{
    using System;

    public class SimpleMessage : ISimpleMessage
    {
        public string MessageId { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public interface ISimpleMessage
    {
        DateTime TimeStamp { get; }
        string MessageId { get; }
    }
}