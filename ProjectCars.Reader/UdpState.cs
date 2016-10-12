using System.Net;
using System.Net.Sockets;

namespace ProjectCars.Reader
{
    public class UdpState
    {
        public UdpState(UdpClient client, IPEndPoint endpoint)
        {
            this.Client = client;
            this.EndPoint = endpoint;
        }

        public UdpClient Client { get; set; }
        public IPEndPoint EndPoint { get; set; }
    }
}
