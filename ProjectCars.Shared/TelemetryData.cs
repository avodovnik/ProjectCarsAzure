using System;
using System.Runtime.InteropServices;

namespace ProjectCars.Shared
{
    public struct TelemetryData
    {
        public ushort BuildVersionNumber;	// 0
        public byte PacketType;	// 2

        // Game states
        public byte GameSessionState;	// 3

        // Participant info
        public sbyte ViewedParticipantIndex;	// 4
        public sbyte NumParticipants;	// 5

        // Unfiltered input
        public byte UnfilteredThrottle;	// 6
        public byte UnfilteredBrake;	// 7
        public sbyte UnfilteredSteering;	// 8
        public byte UnfilteredClutch;	// 9
        public byte RaceStateFlags;	// 10

        // Event information
        public byte LapsInEvent;	// 11

        // Timings
        public Single BestLapTime;	// 12
        public Single LastLapTime;	// 16
        public Single CurrentTime;	// 20
        public Single SplitTimeAhead;	// 24
        public Single SplitTimeBehind;	// 28
        public Single SplitTime;	// 32
        public Single EventTimeRemaining;	// 36
        public Single PersonalFastestLapTime;	// 40
        public Single WorldFastestLapTime;	// 44
        public Single CurrentSector1Time;	// 48
        public Single CurrentSector2Time;	// 52
        public Single CurrentSector3Time;	// 56
        public Single FastestSector1Time;	// 60
        public Single FastestSector2Time;	// 64
        public Single FastestSector3Time;	// 68
        public Single PersonalFastestSector1Time;	// 72
        public Single PersonalFastestSector2Time;	// 76
        public Single PersonalFastestSector3Time;	// 80
        public Single WorldFastestSector1Time;	// 84
        public Single WorldFastestSector2Time;	// 88
        public Single WorldFastestSector3Time;	// 92

        public ushort JoyPad;	// 96

        // Flags
        public byte HighestFlag;	// 98

        // Pit info
        public byte PitModeSchedule;	// 99

        // Car state
        public short OilTempCelsius;	// 100
        public ushort OilPressureKPa;	// 102
        public short WaterTempCelsius;	// 104
        public ushort WaterPressureKpa;	// 106
        public ushort FuelPressureKpa;	// 108
        public byte CarFlags;	// 110
        public byte FuelCapacity;	// 111
        public byte Brake;	// 112
        public byte Throttle;	// 113
        public byte Clutch;	// 114
        public sbyte Steering;	// 115
        public Single FuelLevel;	// 116
        public Single Speed;	// 120
        public ushort Rpm;	// 124
        public ushort MaxRpm;	// 126
        public byte GearNumGears;	// 128
        public byte BoostAmount;	// 129
        public sbyte EnforcedPitStopLap;	// 130
        public byte CrashState;	// 131

        public Single sOdometerKM;	// 132

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] Orientation;	// 136
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] LocalVelocity;	// 148
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] WorldVelocity;	// 160
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] AngularVelocity;	// 172
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] LocalAcceleration;	// 184
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] WorldAcceleration;	// 196
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] ExtentsCentre;	// 208

        // Wheels / Tyres
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] TyreFlags;	// 220
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Terrain;	// 224
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] TyreY;	// 228
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] TyreRPS;	// 244
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] TyreSlipSpeed;	// 260
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] TyreTemp;	// 276
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] TyreGrip;	// 280
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] TyreHeightAboveGround;	// 284
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] TyreLateralStiffness;	// 300
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] TyreWear;	// 316
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] BrakeDamage;	// 320
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] SuspensionDamage;	// 324
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] BrakeTempCelsius;	// 328
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] TyreTreadTemp;	// 336
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] TyreLayerTemp;	// 344
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] TyreCarcassTemp;	// 352
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] TyreRimTemp;	// 360
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] TyreInternalAirTemp;	// 368

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] WheelLocalPositionY;	// 376
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] RideHeight;	// 392
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] SuspensionTravel;	// 408
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] SuspensionVelocity;	// 424
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] AirPressure;	// 440

        // Extras
        public Single EngineSpeed;	// 448
        public Single EngineTorque;	// 452

        // Car damage
        public byte AeroDamage;	// 456
        public byte EngineDamage;	// 457

        // Weather
        public sbyte AmbientTemperature;	// 458
        public sbyte TrackTemperature;	// 459
        public byte RainDensity;	// 460
        public sbyte WindSpeed;	// 461
        public sbyte WindDirectionX;	// 462
        public sbyte WindDirectionY;	// 463

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public ParticipantInfo[] ParticipantInfo;	// 464

        public float TrackLength;	// 1360

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Wings;   // 1364

        public byte DPad;	// 1366
    }
}
