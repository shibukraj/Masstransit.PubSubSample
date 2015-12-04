namespace MassTransit.Subscriber
{
    using System;
    using Topshelf;
    using Topshelf.Logging;

    public class SubscriberService : ServiceControl
    {
        private readonly LogWriter _log = HostLogger.Get<SubscriberService>();

        private readonly IBusControl _busControl;
        private BusHandle _busHandle;


        public SubscriberService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public bool Start(HostControl hostControl)
        {
            _log.Info("Starting the bus .....");
            Console.WriteLine(_busControl.GetProbeResult().ToJsonString());
            _busHandle = _busControl.Start();
            return true;

        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("Stopping the service bus .....");
            _busHandle?.Stop();
            _busHandle?.Dispose();
            _busControl?.Stop();
            return true;
        }

    }
}