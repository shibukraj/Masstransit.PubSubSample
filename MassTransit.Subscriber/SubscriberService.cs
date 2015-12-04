namespace MassTransit.Subscriber
{
    using System;

    public class SubscriberService : ISubscriberService
    {
        public void Start()
        {
            // AutofacBootStrapper.Builder.RegisterModule(new SubscriberBusModule());
            // AutofacBootStrapper.SetUpAutofacContainer();
            var bus = AutofacBootStrapper.GetInstance<IBusControl>();
            bus.Start();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Continue()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }
    }
}