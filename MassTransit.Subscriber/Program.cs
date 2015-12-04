namespace MassTransit.Subscriber
{
    using System.IO;
    using System.Text;
    using Autofac;
    using log4net.Config;
    using Log4NetIntegration.Logging;
    using Topshelf;
    using Topshelf.Logging;

    public class Program
    {
        // http://docs.masstransit-project.com/en/latest/usage/containers/autofac.html
        public static void Main(string[] args)
        {
            ConfigureLogger();

            // Topshelf to use Log4Net
            Log4NetLogWriterFactory.Use();

            // Masstransit to use log4net
            Log4NetLogger.Use();

            //HostFactory.Run(x =>
            //{
            //    x.Service<ServiceControl>(s =>
            //    {
            //        s.BeforeStartingService(sc => sc.RequestAdditionalTime(TimeSpan.FromSeconds(30)));
            //        s.BeforeStoppingService(sc => sc.RequestAdditionalTime(TimeSpan.FromSeconds(30)));

            //        s.ConstructUsing(() =>
            //        {
            //            SetUpContainer();
            //            AutofacBootStrapper.GetInstance<SubscriberService>();
            //        });
            //        s.WhenStarted(svc => svc.Start());
            //        s.WhenStopped(svc => svc.Stop());
            //    });
            //    x.UseLog4Net();
            //    x.StartAutomatically();
            //    x.EnableShutdown();
            //    x.SetServiceName("ServiceName");
            //    x.SetDisplayName("Serice Display Name");
            //    x.SetDescription("Service Description");
            //});

            SetUpContainer();
            HostFactory.Run(cfg =>
            {
                cfg.Service(s => AutofacBootStrapper.GetInstance<SubscriberService>());
                cfg.SetServiceName("Servicename");
                cfg.SetDisplayName("Service Display Name");
                cfg.SetDescription("Service Description");
            });
        }


        private static void SetUpContainer()
        {
            AutofacBootStrapper.Init();
            AutofacBootStrapper.Builder.RegisterModule<SubscriberBusModule>();
            AutofacBootStrapper.SetUpAutofacContainer();
        }

        private static void ConfigureLogger()
        {
            const string logConfig = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                                        <log4net>
                                          <root>
                                            <level value=""DEBUG"" />
                                            <appender-ref ref=""console"" />
                                          </root>
                                          <logger name=""NHibernate"">
                                            <level value=""ERROR"" />
                                          </logger>
                                          <appender name=""console"" type=""log4net.Appender.ColoredConsoleAppender"">
                                            <layout type=""log4net.Layout.PatternLayout"">
                                              <conversionPattern value=""%m%n"" />
                                            </layout>
                                          </appender>
                                        </log4net>";

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(logConfig)))
            {
                XmlConfigurator.Configure(stream);
            }
        }
    }
}