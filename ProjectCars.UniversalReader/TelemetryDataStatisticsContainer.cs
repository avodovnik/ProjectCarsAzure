using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCars.UniversalReader
{
    /// <summary>
    /// Holds information about the packets.
    /// </summary>
    public class TelemetryDataStatisticsContainer
    {
        public long _packetCount = 0;
        public long _unidentifierPacketCont = 0;
        public long _telemetryPacketCount = 0;
        public long _participantInfoPacketCount = 0;
        public long _participantInfoAdditionalPacketCount = 0;
    }
}
