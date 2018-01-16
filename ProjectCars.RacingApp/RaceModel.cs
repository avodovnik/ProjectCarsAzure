using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCars.RacingApp
{
    public enum RaceState
    {
        Starting,
        Racing,
        Finished
    }

    public class RaceModel
    {
        public static RaceModel Current
        {
            get
            {
                return (App.Current as App).RaceModel;
            }
        }

        public RaceModel(string driverName)
        {
            this.DriverName = driverName;
            this.RaceState = RaceState.Starting;
        }

        public string DriverName { get; }

        public void RaceStarted()
        {
            this.RaceState = RaceState.Racing;
            System.Diagnostics.Debug.WriteLine("Race Started");
        }

        public void RaceAborted()
        {
            this.RaceState = RaceState.Finished;
            System.Diagnostics.Debug.WriteLine("Race Aborted");
        }

        public void RaceFinished()
        {
            this.RaceState = RaceState.Finished;
            System.Diagnostics.Debug.WriteLine("Race Finished");
        }

        public RaceState RaceState
        {
            get; private set;
        }

        private void SetupTheListener()
        {

            //string telemetryEventHubName = CustomConfigurationManager.AppSettings["ProjectCars.Telemetry.EHName"];
            //string telemetryconnectionString = CustomConfigurationManager.AppSettings["ProjectCars.Telemetry.EHConnectionString"];
            //string participantInfoEventHubName = CustomConfigurationManager.AppSettings["ProjectCars.ParticipantInfo.EHName"];
            //string participantInfoconnectionString = CustomConfigurationManager.AppSettings["ProjectCars.ParticipantInfo.EHConnectionString"];

            //Debug.WriteLine("Starting to listen...");

            //var listener = new Reader.CarTelemetryReader();

            //// call the listener
            //listener.StartListening();

            //// let's start up the event hub client
            //var ehTelemetryClient = EventHubClient.CreateFromConnectionString(telemetryconnectionString, telemetryEventHubName);
            //var ehParticipantInfoClient = EventHubClient.CreateFromConnectionString(participantInfoconnectionString, participantInfoconnectionString);
            //var stopwatch = new Stopwatch();

            ////TODO manage session IDs better
            //Guid sessionId = Guid.NewGuid();

            //JsonConvert.DefaultSettings = () =>
            //{
            //    JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            //    serializerSettings.Converters.Add(new ByteArrayConverter());
            //    serializerSettings.Formatting = Formatting.None;

            //    return serializerSettings;
            //};

            //new Thread(new ParameterizedThreadStart(DoMonitoring)).Start(listener);

            ////send all three types of packet to the same Event Hub, but use a different publisher for each.
            //listener.OnTelemetryDataReceived = (telemetry =>
            // SendAndMeasure(new SessionWrapper<TelemetryData>(telemetry, sessionId), ehTelemetryClient, stopwatch));

            //listener.OnParticipantInfoStringsReceived = (participantInfoStrings =>
            // SendAndMeasure(new SessionWrapper<ParticipantInfoStrings>(participantInfoStrings, sessionId), ehParticipantInfoClient, stopwatch));

            //listener.OnParticipantInfoStringsAdditionalReceived = (participantInfoStringsAdditional =>
            //   SendAndMeasure(new SessionWrapper<ParticipantInfoStringsAdditional>(participantInfoStringsAdditional, sessionId), ehParticipantInfoClient, stopwatch));

        }
    }
}
