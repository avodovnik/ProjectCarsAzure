using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using PCarsUI.PCarsAPI;
using Newtonsoft.Json;
using PCarsUI.AzureAPI;

namespace PCarsUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 




    public partial class MainWindow : Window
    {

        private Thread telemCaptureThread = null;
        private Boolean activeCapture = false;
        static Mutex syncMutex = new Mutex();
        public static string sessionId;
        private PCarsUDPreader pcarsUDPreader;
        private EventHubSender evHubSender;
        private string evHubConnString = "Endpoint=sb://projectcars-ns.servicebus.windows.net/;SharedAccessKeyName=sender;SharedAccessKey=zoen8S7sCr3+KOPLCFa8xpuyZ3DMDryAI9vqmAK3NGM=";
        private string evHubName = "projectcars-eh";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartCapture_Click(object sender, RoutedEventArgs e)
        {
            StartCaptureButton.IsEnabled = false;
            StopCaptureButton.IsEnabled = true;

            sessionId = Guid.NewGuid().ToString();
            WriteToConsole("Initialising Capture with Session Id:\t" + sessionId);

            // Reset the Power BI dataset
            // pbiMgr = new PBIManager();

            InitialiseCapture();

        }

        private void StopCapture_Click(object sender, RoutedEventArgs e)
        {
            StopCaptureButton.IsEnabled = false;
            StartCaptureButton.IsEnabled = true;    
            activeCapture = false;

            WriteToConsole("Stopping Capture");
        }


        private void Reset_Click(object sender, RoutedEventArgs e)
        {

        }


        public void WriteToConsole(string text)
        {
            // Send the update to the UI thread
            consoleTextBox.Dispatcher.Invoke(new Action(() => consoleTextBox.AppendText(text + "\u2028")));
            consoleTextBox.Dispatcher.Invoke(new Action(() => consoleTextBox.LineDown()));
        }

        private void InitialiseCapture()
        {

            if (pcarsUDPreader == null)
            {
                WriteToConsole("Initialising UDP Reader");
                pcarsUDPreader = new PCarsAPI.PCarsUDPreader();
            }
             if (evHubSender == null)
            {
                WriteToConsole("Initialising Event Hub");
                evHubSender = new EventHubSender();
                evHubSender.Initialise(evHubConnString, evHubName);
                WriteToConsole("Event Hub initialised");
            }

            StartListening();

        }


        private void StartListening()
        {
            if (telemCaptureThread == null)
            {
                // Kick off a thread to start collecting data
                telemCaptureThread = new Thread(FetchData);
                telemCaptureThread.Start();
                
                
            }
            WriteToConsole("Starting capture");
            activeCapture = true;
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
                        Object rawGameData = pcarsUDPreader.ReadGameData();

                        // Lock access to the shared struct
                        syncMutex.WaitOne();

                        // GameDataPacket dataPacket = new GameDataPacket(rawGameData);

                        StructHelper.pCarsAPIStruct shared = ((PCarsStructWrapper)rawGameData).data;
                        shared.timestamp = System.DateTime.UtcNow.ToString("o");
                        shared.sessionId = sessionId;


                        //WriteToConsole(dataPacket.timestamp
                        //    + " Speed: " + (dataPacket.speed).ToString()
                        //    + " Current Time: " + (dataPacket.currentTime).ToString()
                        //    );

                        // Only capture and send the data if the game is running (i.e not paused or in the menu)
                        if (shared.mGameState > 0)
                        {
                            WriteToConsole(DateTime.Now.ToString() + " Game State: " + shared.mGameState);

                            String serializedString = JsonConvert.SerializeObject(shared);
                            Boolean result = evHubSender.Send(serializedString);
                        

                            if (!result)
                            {
                                WriteToConsole("EventHub send failure");
                            }
                            else
                            {
                                WriteToConsole(DateTime.Now.ToString() + " Data Sent");
                            }
                        }
                    }   
                    catch (Exception ex)
                    {
                        WriteToConsole(ex.ToString());
                    }

                    // Release the lock again
                    syncMutex.ReleaseMutex();
                }
            }
        }

    }
}
