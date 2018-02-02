using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCars.UniversalShared
{
    public static class Configuration
    {
        public const int UdpPort = 5606;

        public struct PacketSizes
        {
            public const int ParticipantInfoStrings = 1347;
            public const int ParticipantInfoStringsAdditional = 1028;
            public const int TelemetryData = 1367;
        }
    }
}
