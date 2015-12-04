namespace MassTransit.Subscriber
{
    using System;
    using System.Threading.Tasks;
    using Contracts;

    public class SimpleMessageConsumer : IConsumer<SimpleMessage>
    {
        public Task Consume(ConsumeContext<SimpleMessage> context)
        {
            Console.WriteLine("Consume Application.");
            Console.WriteLine("Message Id - {0}", context.Message.MessageId);
            Console.WriteLine("Date Time is - {0}", context.Message.TimeStamp);
            return Task.FromResult(0);
        }
    }
}