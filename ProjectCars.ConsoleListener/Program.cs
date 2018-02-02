using System;
using System.Diagnostics;
using Microsoft.ServiceBus.Messaging;
using System.Text;
using System.Threading;
using ProjectCars.Shared;
using Newtonsoft.Json;


namespace ProjectCars.ConsoleListener
{
    class Program
    {
        static void Main(string[] args)
        {
            string telemetryEventHubName = CustomConfigurationManager.AppSettings["ProjectCars.Telemetry.EHName"];
            string telemetryconnectionString = CustomConfigurationManager.AppSettings["ProjectCars.Telemetry.EHConnectionString"];
            string participantInfoEventHubName = CustomConfigurationManager.AppSettings["ProjectCars.ParticipantInfo.EHName"];
            string participantInfoconnectionString = CustomConfigurationManager.AppSettings["ProjectCars.ParticipantInfo.EHConnectionString"];
            Console.WriteLine("Starting to listen...");

            var listener = new Reader.CarTelemetryReader();

            // call the listener
            listener.StartListening();

            // let's start up the event hub client
           // var ehTelemetryClient = EventHubClient.CreateFromConnectionString(telemetryconnectionString, telemetryEventHubName);
           // var ehParticipantInfoClient = EventHubClient.CreateFromConnectionString(participantInfoconnectionString, participantInfoconnectionString);
            var stopwatch = new Stopwatch();

            //TODO manage session IDs better
            Guid sessionId = Guid.NewGuid();

            JsonConvert.DefaultSettings = () =>
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                serializerSettings.Converters.Add(new ByteArrayConverter());
                serializerSettings.Formatting = Formatting.None;

                return serializerSettings;
            };
          
            new Thread(new ParameterizedThreadStart(DoMonitoring)).Start(listener);

            //send all three types of packet to the same Event Hub, but use a different publisher for each.
            listener.OnTelemetryDataReceived = (telemetry =>
             SendAndMeasure(new SessionWrapper<TelemetryData>(telemetry, sessionId), ehTelemetryClient, stopwatch));

            listener.OnParticipantInfoStringsReceived = (participantInfoStrings =>
             SendAndMeasure(new SessionWrapper<ParticipantInfoStrings>(participantInfoStrings, sessionId), ehParticipantInfoClient, stopwatch));

            listener.OnParticipantInfoStringsAdditionalReceived = (participantInfoStringsAdditional =>
               SendAndMeasure(new SessionWrapper<ParticipantInfoStringsAdditional>(participantInfoStringsAdditional, sessionId), ehParticipantInfoClient, stopwatch));

            Console.ReadLine();
        }

        private static void SendAndMeasure<T>(SessionWrapper<T> data, EventHubClient ehClient, Stopwatch stopwatch) where T : struct
        {
            stopwatch.Restart();

            string serializedObject = JsonConvert.SerializeObject(data);
            EventData eventData = new EventData(Encoding.UTF8.GetBytes(serializedObject));

            //use SessionId as partition key, in case of multiple sessions running
            eventData.PartitionKey = data.SessionId.ToString();

            ehClient.SendAsync(eventData);

            stopwatch.Stop();
        }

        /// <summary>
        /// Tries to do some basic monitoring.
        /// TODO: would be nice if this were going into AppInsights
        /// </summary>
        /// <param name="rawListener"></param>
        static void DoMonitoring(object rawListener)
        {
            var listener = rawListener as Reader.CarTelemetryReader;
            if (listener == null)
            {
                throw new InvalidCastException("The raw listener passed in isn't supported");
            }
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
