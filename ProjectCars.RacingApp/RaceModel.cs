using ProjectCars.UniversalShared;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using ProjectCars.UniversalReader;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

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
        private Guid _id;

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
            this._id = Guid.NewGuid();
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

            _listener.OnTelemetryDataReceived = (telemetry => callback(telemetry));

            return;       
            

            var connectionStringBuilder = new EventHubsConnectionStringBuilder(telemetryconnectionString)
            {
                EntityPath = telemetryEventHubName
            };

            var ehConnStringParticipants = new EventHubsConnectionStringBuilder(participantInfoconnectionString)
            {
                EntityPath = participantInfoEventHubName
            };

            // let's start up the event hub client
            var ehTelemetryClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            var ehParticipantClient = EventHubClient.CreateFromConnectionString(participantInfoconnectionString.ToString());

            var stopwatch = new Stopwatch();

            var sessionId = _id;

            JsonConvert.DefaultSettings = () =>
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                serializerSettings.Converters.Add(new ByteArrayConverter());
                serializerSettings.Formatting = Formatting.None;

                return serializerSettings;
            };

            //send all three types of packet to the same Event Hub, but use a different publisher for each.
            _listener.OnTelemetryDataReceived = (telemetry =>
            {
                SendAndMeasure(new SessionWrapper<TelemetryData>(telemetry, sessionId), ehTelemetryClient, stopwatch);
                if (callback != null)
                {
                    callback(telemetry);
                }
            });

            _listener.OnParticipantInfoStringsReceived = (participantInfoStrings =>
             SendAndMeasure(new SessionWrapper<ParticipantInfoStrings>(participantInfoStrings, sessionId), ehParticipantClient, stopwatch));

            _listener.OnParticipantInfoStringsAdditionalReceived = (participantInfoStringsAdditional =>
               SendAndMeasure(new SessionWrapper<ParticipantInfoStringsAdditional>(participantInfoStringsAdditional, sessionId), ehParticipantClient, stopwatch));
        }

        private static void SendAndMeasure<T>(SessionWrapper<T> data, EventHubClient ehClient, Stopwatch stopwatch) where T : struct
        {
            stopwatch.Restart();

            string serializedObject = JsonConvert.SerializeObject(data);
            EventData eventData = new EventData(Encoding.UTF8.GetBytes(serializedObject));

            //use SessionId as partition key, in case of multiple sessions running
            //eventData.SystemProperties.PartitionKey = data.SessionId.ToString();

            ehClient.SendAsync(eventData);

            stopwatch.Stop();
        }
    }
}
