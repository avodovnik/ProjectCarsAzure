﻿using ProjectCars.Shared;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace ProjectCars.Reader
{
    /// <summary>
    /// We'll use this class to read the telemetry data from.
    /// </summary>
    public class CarTelemetryReader
    {       
        // holds our udp client
        private UdpClient _client;
        private IPEndPoint _endpoint = new IPEndPoint(IPAddress.Any, Shared.Configuration.UdpPort);
        private TelemetryDataStatisticsContainer _statistics = new TelemetryDataStatisticsContainer();

        public CarTelemetryReader()
        {
            _client = new UdpClient(Endpoint);
            _client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        public Action<TelemetryData> Callback { get; set; }

        public void StartListening()
        {
            // we just forward the call with our internal method
            Client.BeginReceive(new AsyncCallback(ReceiveCallback), new UdpState(Client, Endpoint));
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            var state = (UdpState)ar.AsyncState;
            var client = state.Client;

            var endpoint = state.EndPoint;
            var receiveBytes = client.EndReceive(ar, ref endpoint);

            // we want to do some "small" advance work on this, to save processing in the 
            // called function
            // the first two bytes are version info, and we can ignore it
            int frameTypeAndSequence = receiveBytes[2];
            int frameType = frameTypeAndSequence & 3;
            int sequence = frameTypeAndSequence >> 2;

            Console.WriteLine("Packet received, sequence: {0}", sequence);

            ProcessIncomingPacket(receiveBytes, frameType);

            // Restart listening for udp data packages
            client.BeginReceive(new AsyncCallback(ReceiveCallback), ar.AsyncState);
        }

        private void ProcessIncomingPacket(byte[] receiveBytes, int frameType)
        {
            // increase the packet count statistic
            Interlocked.Increment(ref _statistics._packetCount);

            byte[] packet; // will hold actual packet data, stripped out from the buffer

            switch(frameType)
            {
                case 0: // telemetry data
                    Interlocked.Increment(ref _statistics._telemetryPacketCount);

                    packet = GetArraySubset(Shared.Configuration.PacketSizes.TelemetryData, receiveBytes);
                    ProcessPacketAsTelemetryData(packet);
                    break;

                case 1: // participant info
                    Interlocked.Increment(ref _statistics._participantInfoPacketCount);

                    packet = GetArraySubset(Shared.Configuration.PacketSizes.ParticipantInfoStrings, receiveBytes);
                    ProcessPacketAsParticipantInfo(packet);
                    break;

                case 2: // participant info additional
                    Interlocked.Increment(ref _statistics._participantInfoAdditionalPacketCount);

                    packet = GetArraySubset(Shared.Configuration.PacketSizes.ParticipantInfoStringsAdditional, receiveBytes);
                    ProcessPacketAsAdditionalParticipantInfo(packet);

                    break;

                default:
                    // unsupported packet, now what? 
                    System.Diagnostics.Debug.WriteLine("An unsupported packet was captured.");
                    Interlocked.Increment(ref _statistics._unidentifierPacketCont);

                    break;
            }
        }


        private byte[] GetArraySubset(int len, byte[] rawBytes)
        {
            var packet = new byte[len];
            Array.Copy(rawBytes, packet, len);

            return packet;
        }

        private void ProcessPacketAsTelemetryData(byte[] packet)
        {
            var telemetryData = MarshalToStructure<TelemetryData>(packet);
            Console.WriteLine("Telemetry packet received: {0}, total packet count: {1}", telemetryData.sThrottle, _statistics._telemetryPacketCount);

            // do something with this packet
            if(Callback != null)
            {
                Callback(telemetryData);
            }
        }

        private void ProcessPacketAsAdditionalParticipantInfo(byte[] packet)
        {
            //throw new NotImplementedException();
        }

        private void ProcessPacketAsParticipantInfo(byte[] packet)
        {
            //throw new NotImplementedException();
        }

        private T MarshalToStructure<T>(byte[] mem)
        {
            var handle = GCHandle.Alloc(mem, GCHandleType.Pinned);
            try
            {
                var data = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                return data;
            }
            finally
            {
                // we want to make sure this is called! even if there's an exception
                handle.Free(); // free up memory
            }
            throw new NotImplementedException();
        }

        private UdpClient Client { get { return _client; } }
        private IPEndPoint Endpoint { get { return _endpoint; } }
    }
}
