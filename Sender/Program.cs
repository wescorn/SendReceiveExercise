using EasyNetQ;
using Messages;

public class Program
{
    public static void Main(string[] args)
    {
        using(var activity = Monitoring.Monitoring.ActivitySource.StartActivity())
        {
            var bus = RabbitHutch.CreateBus("host=localhost;username=application;password=pepsi");
            var message = new Message("Hello World");
            bus.PubSub.PublishAsync(message);
        }
        Thread.Sleep(5000); // Give Zipkin time to receive the event
    }
}