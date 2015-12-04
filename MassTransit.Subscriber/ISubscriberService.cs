namespace MassTransit.Subscriber
{
    public interface ISubscriberService
    {
        void Start();
        void Stop();
    }
}