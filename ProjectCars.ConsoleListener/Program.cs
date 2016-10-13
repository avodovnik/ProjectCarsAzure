using System;
using System.Diagnostics;
using Microsoft.ServiceBus.Messaging;
using System.Text;
using System.Threading;

namespace ProjectCars.ConsoleListener
{
    class Program
    {
        static void Main(string[] args)
        {
            string eventHubName = "Cars";
            string connectionString = Shared.CustomConfigurationManager.AppSettings["ProjectCars.EHConnectionString"];

            Console.WriteLine("Starting to listen...");

            var listener = new ProjectCars.Reader.CarTelemetryReader();

            // call the listener
            listener.StartListening();

            // let's start up the event hub client
            var ehClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);

            var stopwatch = new Stopwatch();

            new Thread(new ParameterizedThreadStart(DoMonitoring)).Start(listener);

            listener.OnTelemetryDataReceived = (p =>
            {
                stopwatch.Restart();

                var s = Newtonsoft.Json.JsonConvert.SerializeObject(p, Newtonsoft.Json.Formatting.None);
                ehClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(s)));
                stopwatch.Stop();
            });


            Console.ReadLine();
        }

        /// <summary>
        /// Tries to do some basic monitoring.
        /// TODO: would be nice if this were going into AppInsights
        /// </summary>
        /// <param name="rawListener"></param>
        static void DoMonitoring(object rawListener)
        {
            var listener = rawListener as ProjectCars.Reader.CarTelemetryReader;
            if (listener == null)
                throw new InvalidCastException("The raw listener passed in isn't supported");

            var stopwatch = new Stopwatch();

            while (true)
            {
                var startingCount = Interlocked.Read(ref listener.Statistics._telemetryPacketCount);
                stopwatch.Start();

                Thread.Sleep(500);

                stopwatch.Stop();

                var endingCount = Interlocked.Read(ref listener.Statistics._telemetryPacketCount);

                var rate = ((endingCount - startingCount)*1000) / stopwatch.ElapsedMilliseconds;

                Console.Clear();

                Console.WriteLine("Total packets sent: {0}", endingCount);
                Console.WriteLine("Current rate: {0} packets/s", rate);

                stopwatch.Reset();
            }
        }


    }
}
