namespace MassTransit.Publisher
{
    using System;
    using Contracts;
    using Log4NetIntegration.Logging;

    public class Program
    {
        public static void Main()
        {
            Log4NetLogger.Use();
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                x.Host(new Uri("rabbitmq://10.208.7.73"), h =>
                {
                    h.Username("appadmin");
                    h.Password("ohl123");
                });
            });

            var handle = bus.Start();

            var text = "";

            while (text != "quit")
            {
                Console.WriteLine("Enter a message : ");
                text = Console.ReadLine();
                var msg = new SimpleMessage
                {
                    MessageId = text,
                    TimeStamp = DateTime.Now
                };
                bus.Publish(msg);
            }

            handle.StopAsync().Wait();
        }
    }
}