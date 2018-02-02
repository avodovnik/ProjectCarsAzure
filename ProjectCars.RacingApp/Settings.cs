using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ProjectCars.RacingApp
{
    public class Settings : ObservableSettings
    {
        private static Settings settings = new Settings();
        public static Settings Default
        {
            get { return settings; }
        }

        public Settings()
            : base(ApplicationData.Current.LocalSettings)
        {
        }

        public string TelemetryEventHubName
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string TelemetryConnectionString
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string ParticipantInfoEventHubName
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string ParticipantInfoconnectionString
        {
            get { return Get<string>(); }
            set { Set(value); }
        }
    }

}
