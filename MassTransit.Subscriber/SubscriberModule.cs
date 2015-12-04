#region File Header

namespace MassTransit.Subscriber
{
    using Autofac;

    public class SubscriberModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SubscriberService>();
        }
    }
}