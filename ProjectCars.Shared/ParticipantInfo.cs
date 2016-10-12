using System;
using System.Runtime.InteropServices;

namespace ProjectCars.Shared
{
    public struct ParticipantInfo
    {
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] sWorldPosition;	// 0
        public ushort sCurrentLapDistance;	// 6
        public byte sRacePosition;	// 8
        public byte sLapsCompleted;	// 9
        public byte sCurrentLap;	// 10
        public byte sSector;	// 11
        public Single sLastSectorTime;	// 14
        // 16
    }
}
