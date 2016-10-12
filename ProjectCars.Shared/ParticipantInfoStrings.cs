using System.Runtime.InteropServices;

namespace ProjectCars.Shared
{
    public struct ParticipantInfoStrings
    {
        public ushort sBuildVersionNumber;	// 0
        public byte sPacketType;	// 2
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string sCarName;	// 3
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string sCarClassName;	// 131
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string sTrackLocation;	// 195        
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string sTrackVariation;	// 259
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16)]
        public ParticipantNameString[] sName;  // 323
                                    // 1347
    }

    public struct ParticipantInfoStringsAdditional
    {
        public ushort sBuildVersionNumber;	// 0
        public byte sPacketType;	// 2
        public byte sOffset;	// 3
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16)]
        public ParticipantNameString[] sName;	// 4
        // 1028
    }

    public struct ParticipantNameString
    {
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] nameByteArray;
    }


}
