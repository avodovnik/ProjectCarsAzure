﻿using System;
using System.Runtime.InteropServices;

namespace ProjectCars.Shared
{
    public struct TelemetryData
    {
        public ushort sBuildVersionNumber;	// 0
        public byte sPacketType;	// 2

        // Game states
        public byte sGameSessionState;	// 3

        // Participant info
        public sbyte sViewedParticipantIndex;	// 4
        public sbyte sNumParticipants;	// 5

        // Unfiltered input
        public byte sUnfilteredThrottle;	// 6
        public byte sUnfilteredBrake;	// 7
        public sbyte sUnfilteredSteering;	// 8
        public byte sUnfilteredClutch;	// 9
        public byte sRaceStateFlags;	// 10

        // Event information
        public byte sLapsInEvent;	// 11

        // Timings
        public Single sBestLapTime;	// 12
        public Single sLastLapTime;	// 16
        public Single sCurrentTime;	// 20
        public Single sSplitTimeAhead;	// 24
        public Single sSplitTimeBehind;	// 28
        public Single sSplitTime;	// 32
        public Single sEventTimeRemaining;	// 36
        public Single sPersonalFastestLapTime;	// 40
        public Single sWorldFastestLapTime;	// 44
        public Single sCurrentSector1Time;	// 48
        public Single sCurrentSector2Time;	// 52
        public Single sCurrentSector3Time;	// 56
        public Single sFastestSector1Time;	// 60
        public Single sFastestSector2Time;	// 64
        public Single sFastestSector3Time;	// 68
        public Single sPersonalFastestSector1Time;	// 72
        public Single sPersonalFastestSector2Time;	// 76
        public Single sPersonalFastestSector3Time;	// 80
        public Single sWorldFastestSector1Time;	// 84
        public Single sWorldFastestSector2Time;	// 88
        public Single sWorldFastestSector3Time;	// 92

        public ushort sJoyPad;	// 96

        // Flags
        public byte sHighestFlag;	// 98

        // Pit info
        public byte sPitModeSchedule;	// 99

        // Car state
        public short sOilTempCelsius;	// 100
        public ushort sOilPressureKPa;	// 102
        public short sWaterTempCelsius;	// 104
        public ushort sWaterPressureKpa;	// 106
        public ushort sFuelPressureKpa;	// 108
        public byte sCarFlags;	// 110
        public byte sFuelCapacity;	// 111
        public byte sBrake;	// 112
        public byte sThrottle;	// 113
        public byte sClutch;	// 114
        public sbyte sSteering;	// 115
        public Single sFuelLevel;	// 116
        public Single sSpeed;	// 120
        public ushort sRpm;	// 124
        public ushort sMaxRpm;	// 126
        public byte sGearNumGears;	// 128
        public byte sBoostAmount;	// 129
        public sbyte sEnforcedPitStopLap;	// 130
        public byte sCrashState;	// 131

        public Single sOdometerKM;	// 132

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] sOrientation;	// 136
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] sLocalVelocity;	// 148
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] sWorldVelocity;	// 160
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] sAngularVelocity;	// 172
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] sLocalAcceleration;	// 184
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] sWorldAcceleration;	// 196
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
        public Single[] sExtentsCentre;	// 208

        // Wheels / Tyres
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] sTyreFlags;	// 220
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] sTerrain;	// 224
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sTyreY;	// 228
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sTyreRPS;	// 244
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sTyreSlipSpeed;	// 260
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] sTyreTemp;	// 276
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] sTyreGrip;	// 280
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sTyreHeightAboveGround;	// 284
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sTyreLateralStiffness;	// 300
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] sTyreWear;	// 316
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] sBrakeDamage;	// 320
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] sSuspensionDamage;	// 324
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] sBrakeTempCelsius;	// 328
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] sTyreTreadTemp;	// 336
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] sTyreLayerTemp;	// 344
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] sTyreCarcassTemp;	// 352
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] sTyreRimTemp;	// 360
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] sTyreInternalAirTemp;	// 368

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sWheelLocalPositionY;	// 376
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sRideHeight;	// 392
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sSuspensionTravel;	// 408
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public Single[] sSuspensionVelocity;	// 424
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] sAirPressure;	// 440

        // Extras
        public Single sEngineSpeed;	// 448
        public Single sEngineTorque;	// 452

        // Car damage
        public byte sAeroDamage;	// 456
        public byte sEngineDamage;	// 457

        // Weather
        public sbyte sAmbientTemperature;	// 458
        public sbyte sTrackTemperature;	// 459
        public byte sRainDensity;	// 460
        public sbyte sWindSpeed;	// 461
        public sbyte sWindDirectionX;	// 462
        public sbyte sWindDirectionY;	// 463

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public ParticipantInfo[] sParticipantInfo;	// 464

        public float sTrackLength;	// 1360

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] sWings;   // 1364

        public byte sDPad;	// 1366
    }
}
