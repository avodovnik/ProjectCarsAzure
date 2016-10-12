using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCars.Reader
{
    /// <summary>
    /// Holds information about the packets.
    /// </summary>
    public class TelemetryDataStatisticsContainer
    {
        public int _packetCount = 0;
        public int _unidentifierPacketCont = 0;
        public int _telemetryPacketCount = 0;
        public int _participantInfoPacketCount = 0;
        public int _participantInfoAdditionalPacketCount = 0;
    }
}
