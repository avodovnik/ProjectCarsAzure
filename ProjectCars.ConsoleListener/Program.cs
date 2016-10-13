using System;
using System.Diagnostics;
using Microsoft.ServiceBus.Messaging;
using System.Text;

namespace ProjectCars.ConsoleListener
{
    class Program
    {         
        static void Main(string[] args)
        {
            string eventHubName = "Cars";
            string connectionString = "Endpoint=sb://anze-eventhub.servicebus.windows.net/;SharedAccessKeyName=CarsSender;SharedAccessKey=uO2xqOL+4IHRppOqnHy3TeQVmb8ewYgH5HFTqiqTdDU=";

            Console.WriteLine("Starting to listen...");

            var listener = new ProjectCars.Reader.CarTelemetryReader();

            // call the listener
            listener.StartListening();

            // let's start up the event hub client
            var ehClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
            
            

            var stopwatch = new Stopwatch();

            listener.OnTelemetryDataReceived = (p =>
            {
                stopwatch.Restart();
                
                var s = Newtonsoft.Json.JsonConvert.SerializeObject(p, Newtonsoft.Json.Formatting.None);
                ehClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(s)));
                stopwatch.Stop();
                Console.WriteLine("Packet length JSON: {0}, took {1} ms to serialize and send.pl", s.Length, stopwatch.ElapsedMilliseconds);
            });

            Console.ReadLine();
        }

        

    }
}
