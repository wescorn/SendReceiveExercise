using EasyNetQ;
using Messages;
using OpenTelemetry.Context.Propagation;

public class Program
{
    private static readonly TextMapPropagator Propagator = new TraceContextPropagator();
    
    public static void Main(string[] args)
    {
        var bus = RabbitHutch.CreateBus("host=localhost;username=application;password=pepsi");
        bus.PubSub.SubscribeAsync<Message>("DTE", msg =>
        {
            using (var activity = Monitoring.Monitoring.ActivitySource.StartActivity())
            {
                Console.WriteLine(msg.Text);
            }
        });
        
        while(true) { Thread.Sleep(5000); }
    }
}