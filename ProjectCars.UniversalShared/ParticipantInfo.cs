using System;
using System.Runtime.InteropServices;

namespace ProjectCars.UniversalShared
{
    public struct ParticipantInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] WorldPosition;	// 0
        public ushort CurrentLapDistance;	// 6
        public byte RacePosition;	// 8
        public byte LapsCompleted;	// 9
        public byte CurrentLap;	// 10
        public byte Sector;	// 11
        public Single LastSectorTime;	// 14
        // 16
    }
}
