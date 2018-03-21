using System;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class UdpHandler
    {
        private string host{get;}
        private int port { get; }

        public UdpHandler(string host, int port)
        {
            this.host = host;
            this.port = port;
        }
        
        public async Task<int> SendAsync(string str)
        {
            return await SendAsync(Encoding.UTF8.GetBytes(str)).ConfigureAwait(false);
        }


        public async Task<int> SendAsync(byte[] data) {

            using (var udpClient = new System.Net.Sockets.UdpClient())
            {
                var result = await udpClient.SendAsync(data, data.Length, host, port).ConfigureAwait(false);

                return result;
            }
        }

    }
}
