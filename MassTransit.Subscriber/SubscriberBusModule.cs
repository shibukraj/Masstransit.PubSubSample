namespace MassTransit.Subscriber
{
    using System;
    using Autofac;

    public class SubscriberBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://10.208.7.73"), h =>
                {
                    h.Username("appadmin");
                    h.Password("ohl123");
                });
                sbc.ReceiveEndpoint(host, "sraj_pubsubtest", ep =>
                {
                    ep.Durable = true;
                    ep.PrefetchCount = 10;
                    ep.Consumer<SimpleMessageConsumer>();
                });
            })).SingleInstance().As<IBus>().As<IBusControl>();


            //    sbc.ReceiveEndpoint(host,"sraj_pubsubtest", ec =>
            //    {
            //        ec.LoadFrom(ctx);
            //    });
            //})).SingleInstance().As<IBusControl>().As<IBus>();
        }
    }
}