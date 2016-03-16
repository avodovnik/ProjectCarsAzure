using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace PCarsUI.AzureAPI
{
    class EventHubSender
    {

        private EventHubClient eventHubClient;
        private static string eventHubName;
        public Boolean initialised = false;

        public void Initialise(string connectionString, string eventHubName)
        {
            if (!initialised)
            {

                eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);

                initialised = true;
                return;
            }

            return;
        }

        public Boolean Send(string data)
        {
            if (initialised)
            {
                EventData encodedData = new EventData(Encoding.UTF8.GetBytes(data));
                eventHubClient.SendAsync(encodedData);
                return true;
            }
            return false;
        }
    }
}
