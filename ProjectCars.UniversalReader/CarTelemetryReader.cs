using ProjectCars.UniversalShared;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectCars.UniversalReader
{
    /// <summary>
    /// We'll use this class to read the telemetry data from.
    /// </summary>
    public class CarTelemetryReader
    {
        // holds our udp client
        private UdpClient _client;
        private IPEndPoint _endpoint = new IPEndPoint(IPAddress.Any, UniversalShared.Configuration.UdpPort);
        private TelemetryDataStatisticsContainer _statistics = new TelemetryDataStatisticsContainer();

        public CarTelemetryReader()
        {
            _client = new UdpClient(Endpoint);
            _client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        public Action<TelemetryData> OnTelemetryDataReceived { get; set; }
        public Action<ParticipantInfoStrings> OnParticipantInfoStringsReceived { get; set; }
        public Action<ParticipantInfoStringsAdditional> OnParticipantInfoStringsAdditionalReceived { get; set; }

        public async void StartListeningAsync()
        {
            try
            {
                while (_listenForPackets)
                {
                    var result = await Client.ReceiveAsync();
                    //ReceiveCallback(result);
                    ParsePacket(result);
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StopListening()
        {
            _listenForPackets = false;
        }

        private bool _listenForPackets = true;

        private void ParsePacket(UdpReceiveResult result)
        {
            Debug.WriteLine("Parse Packet called.");
            if(result != null)
            {
                return;
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var receiveBytes = result.Buffer;
            // we want to do some "small" advance work on this, to save processing in the 
            // called function
            // the first two bytes are version info, and we can ignore it
            int frameTypeAndSequence = receiveBytes[2];
            int frameType = frameTypeAndSequence & 3;
            int sequence = frameTypeAndSequence >> 2;

            ProcessIncomingPacket(receiveBytes, frameType);

            stopwatch.Stop();

            System.Diagnostics.Debug.WriteLine(String.Format("It took {0}ms to process an incoming packet.", stopwatch.ElapsedMilliseconds));
            // Restart listening for udp data packages
        }

   
        private void ProcessIncomingPacket(byte[] receiveBytes, int frameType)
        {
            // increase the packet count statistic
            Interlocked.Increment(ref _statistics._packetCount);

            byte[] packet; // will hold actual packet data, stripped out from the buffer

            switch (frameType)
            {
                case 0: // telemetry data
                    Interlocked.Increment(ref _statistics._telemetryPacketCount);

                    packet = GetArraySubset(UniversalShared.Configuration.PacketSizes.TelemetryData, receiveBytes);
                    ProcessPacketAsTelemetryData(packet);
                    break;

                case 1: // participant info
                    Interlocked.Increment(ref _statistics._participantInfoPacketCount);

                    packet = GetArraySubset(UniversalShared.Configuration.PacketSizes.ParticipantInfoStrings, receiveBytes);
                    ProcessPacketAsParticipantInfo(packet);
                    break;

                case 2: // participant info additional
                    Interlocked.Increment(ref _statistics._participantInfoAdditionalPacketCount);

                    packet = GetArraySubset(UniversalShared.Configuration.PacketSizes.ParticipantInfoStringsAdditional, receiveBytes);
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

            // do something with this packet
            OnTelemetryDataReceived?.Invoke(telemetryData);
        }

        private void ProcessPacketAsAdditionalParticipantInfo(byte[] packet)
        {
            var additionalParticipantInfo = MarshalToStructure<ParticipantInfoStringsAdditional>(packet);
            Console.WriteLine("Additional Participant Info Receive: {0}, packet count: {1}", additionalParticipantInfo.Names.First().Name, _statistics._participantInfoAdditionalPacketCount);

            OnParticipantInfoStringsAdditionalReceived?.Invoke(additionalParticipantInfo);
        }

        private void ProcessPacketAsParticipantInfo(byte[] packet)
        {
            var participantInfo = MarshalToStructure<ParticipantInfoStrings>(packet);
            Console.WriteLine("Participant Info Receive: {0}, packet count: {1}", participantInfo.CarName, _statistics._participantInfoPacketCount);

            OnParticipantInfoStringsReceived?.Invoke(participantInfo);
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
        public TelemetryDataStatisticsContainer Statistics { get { return _statistics; } }
    }
}
