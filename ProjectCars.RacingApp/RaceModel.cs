using ProjectCars.UniversalShared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCars.UniversalReader;

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
        private CarTelemetryReader _listener;
        private Task _task;

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

        public void RaceStarted(Action<TelemetryData> callbackAction)
        {
            this.RaceState = RaceState.Racing;
            System.Diagnostics.Debug.WriteLine("Race Started");
            this.SetupTheListener(callbackAction);

        }


        public void RaceAborted()
        {
            StopListening();
            this.RaceState = RaceState.Finished;
            System.Diagnostics.Debug.WriteLine("Race Aborted");
        }

        public void RaceFinished()
        {
            StopListening();
            this.RaceState = RaceState.Finished;
            System.Diagnostics.Debug.WriteLine("Race Finished");
        }

        private void StopListening()
        {
            _listener.StopListening();
        }

        public RaceState RaceState
        {
            get; private set;
        }

        private void SetupTheListener(Action<TelemetryData> callback)
        {

            string telemetryEventHubName = App.Settings.TelemetryEventHubName;
            string telemetryconnectionString = App.Settings.TelemetryConnectionString;
            string participantInfoEventHubName = App.Settings.ParticipantInfoEventHubName;
            string participantInfoconnectionString = App.Settings.ParticipantInfoconnectionString;

            Debug.WriteLine("Starting to listen...");

            _listener = new UniversalReader.CarTelemetryReader();

            _task = Task.Run(() => _listener.StartListeningAsync());

            _listener.OnTelemetryDataReceived = callback;
            
            ///istener.OnTelemetryDataReceived = (telemetry => )

            ////// let's start up the event hub client
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

            //send all three types of packet to the same Event Hub, but use a different publisher for each.
            //listener.OnTelemetryDataReceived = (telemetry =>
            // SendAndMeasure(new SessionWrapper<TelemetryData>(telemetry, sessionId), ehTelemetryClient, stopwatch));

            //listener.OnParticipantInfoStringsReceived = (participantInfoStrings =>
            // SendAndMeasure(new SessionWrapper<ParticipantInfoStrings>(participantInfoStrings, sessionId), ehParticipantInfoClient, stopwatch));

            //listener.OnParticipantInfoStringsAdditionalReceived = (participantInfoStringsAdditional =>
            //   SendAndMeasure(new SessionWrapper<ParticipantInfoStringsAdditional>(participantInfoStringsAdditional, sessionId), ehParticipantInfoClient, stopwatch));

            //_listener.OnTelemetryDataReceived = (telemetry =>
            //    System.Diagnostics.Debug.WriteLine(String.Format("Telemetry data received, speed: {0}", telemetry.Speed))
            //);
        }
    }
}
