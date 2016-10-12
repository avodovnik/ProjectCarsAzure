using ProjectCars.Reader;
using System;
using System.Diagnostics;
using System.Text;

namespace ProjectCars.ConsoleListener
{
    class Program
    {         
        static void Main(string[] args)
        {
            Console.WriteLine("Starting to listen...");

            var listener = new ProjectCars.Reader.CarTelemetryReader();

            // call the listener
            listener.StartListening();

            var stopwatch = new Stopwatch();
            listener.Callback = (p =>
            {
                stopwatch.Restart();
                var s = Newtonsoft.Json.JsonConvert.SerializeObject(p, Newtonsoft.Json.Formatting.None);
                stopwatch.Stop();
                Console.WriteLine("Packet length JSON: {0}, took {1} ms to serialize.", s.Length, stopwatch.ElapsedMilliseconds);
            });

            Console.ReadLine();
        }

        

    }
}
