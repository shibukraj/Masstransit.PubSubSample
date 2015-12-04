namespace MassTransit.Subscriber
{
    using System;
    using Autofac;
    using Topshelf;

    public class Program
    {
        // http://docs.masstransit-project.com/en/latest/usage/containers/autofac.html
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ISubscriberService>(s =>
                {
                    s.BeforeStartingService(sc => sc.RequestAdditionalTime(TimeSpan.FromSeconds(30)));
                    s.BeforeStoppingService(sc => sc.RequestAdditionalTime(TimeSpan.FromSeconds(30)));
                    s.ConstructUsing(() =>
                    {
                        SetUpContainer();
                        return AutofacBootStrapper.GetInstance<SubscriberService>();
                    });
                    s.WhenStarted(svc => svc.Start());
                    s.WhenStopped(svc => svc.Stop());
                });
                x.UseLog4Net();
                x.StartAutomatically();
                x.EnableShutdown();
                x.SetServiceName("ServiceName");
                x.SetDisplayName("Serice Display Name");
                x.SetDescription("Service Description");
            });
        }


        private static void SetUpContainer()
        {
            AutofacBootStrapper.Init();
            AutofacBootStrapper.Builder.RegisterModule<SubscriberBusModule>();
            AutofacBootStrapper.Builder.RegisterModule<SubscriberModule>();

            AutofacBootStrapper.SetUpAutofacContainer();
        }
    }
}