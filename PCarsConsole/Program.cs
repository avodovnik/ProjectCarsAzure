using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PCarsConsole.PCarsAPI;
using Newtonsoft.Json;

namespace PCarsConsole
{
    class PCarsTelemetryCapture
    {

       // public static string sessionId;
        // This is the IP endpoint we are connecting to (i.e. the IP Address and Port the game is sending to)     
       // private IPEndPoint remoteIP;
        // This is the IP endpoint for capturing who actually sent the data
       // private IPEndPoint senderIP = new IPEndPoint(IPAddress.Any, 0);
       // UDP Socket for the connection
       // private UdpClient udpListener;

        private Thread telemCaptureThread = null;
        private Boolean activeCapture = false;
        static Mutex syncMutex = new Mutex();


        //public GameStateData currentGameState = null;


        private PCarsAPI.PCarsUDPreader pcarsUDPreader;

        static void Main(string[] args)
        {
            PCarsTelemetryCapture capture = new PCarsTelemetryCapture();
            Console.WriteLine("Press an key to start capture");
            Console.ReadKey();


            
            capture.InitialiseUDPReader();
            // GameStateReaderFactory.getInstance().getGameStateReader();
            capture.StartListening();
        }

        private void InitialiseUDPReader()
        {
            if (pcarsUDPreader == null)
            {
                pcarsUDPreader = new PCarsAPI.PCarsUDPreader();
                //pcarsUDPreader.Initialise();
                return;
            }
        }

        private void StartListening()
        {
            if (telemCaptureThread == null)
            {
                // Kick off a thread to start collecting data
                telemCaptureThread = new Thread(FetchData);
                telemCaptureThread.Start();
                Console.WriteLine("Starting capture...");
                activeCapture = true;
            }
        }

        private void FetchData()
        {
            //eventCount = 0;
            while (true)
            {
                if (activeCapture)
                {
                    try
                    {

                        // Get the data (this will block until we get a packet)
                        // Byte[] receiveBytes = udpSocket.Receive(ref senderIP);
                        Object rawGameData = pcarsUDPreader.ReadGameData();

                        //(PCarsStructWrapper)data.

                        StructHelper.pCarsAPIStruct shared = ((PCarsStructWrapper)rawGameData).data;


                        Console.WriteLine("Speed: " + (shared.mSpeed).ToString());
                        Console.WriteLine("RPM: " + (shared.mRPM).ToString());
                        Console.Clear();
                        String serializedString = JsonConvert.SerializeObject(shared);



                        // Lock access to the shared struct
                        syncMutex.WaitOne();



                        // Send to the Event Hub -- 
                       
                   //     EventData data = new EventData(Encoding.UTF8.GetBytes(serializedString));
                   //     eventHubClient.SendAsync(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    // Release the lock again
                    syncMutex.ReleaseMutex();
                }
            }
        }

    }
}
