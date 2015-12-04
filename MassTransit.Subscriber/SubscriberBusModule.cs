namespace MassTransit.Subscriber
{
    using System;
    using Autofac;

    public class SubscriberBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register all cosumers with the container.
            builder.RegisterType<SubscriberService>();

            // register the bus
            builder.Register(ctx => Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var componentContext = ctx.Resolve<IComponentContext>();

                var host = sbc.Host(new Uri("rabbitmq://10.208.7.73"), h =>
                {
                    h.Username("appadmin");
                    h.Password("ohl123");
                });
                sbc.ReceiveEndpoint(host, "sraj_pubsubtest", ep =>
                {
                    ep.Durable = true;
                    ep.PrefetchCount = 10;
                    ep.LoadFrom(componentContext.Resolve<ILifetimeScope>());
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