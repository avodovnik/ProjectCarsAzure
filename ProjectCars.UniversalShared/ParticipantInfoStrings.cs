using System.Runtime.InteropServices;

namespace ProjectCars.UniversalShared
{
    public struct ParticipantInfoStrings
    {
        public ushort sBuildVersionNumber;	// 0
        public byte PacketType;	// 2
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string CarName;	// 3
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string CarClassName;	// 131
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string TrackLocation;	// 195        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string TrackVariation;	// 259
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public ParticipantNameString[] Name;  // 323
                                    // 1347
    }

    public struct ParticipantInfoStringsAdditional
    {
        public ushort sBuildVersionNumber;	// 0
        public byte sPacketType;	// 2
        public byte sOffset;	// 3
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public ParticipantNameString[] Names;	// 4
        // 1028
    }

    public struct ParticipantNameString
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Name;
    }


}
