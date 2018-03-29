using System;
using System.IO;
using System.Net;
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
        
        public static async Task<bool> ShootLumix (string ipaddr)
        {

            string url = $"http://{ipaddr}/cam.cgi?mode=camcmd&value=capture";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }



            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.

            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect))
            {


                // if the remote file was found, download it
                using (Stream inputStream = response.GetResponseStream())
                using (MemoryStream memStream = new MemoryStream())
                {
                    await inputStream.CopyToAsync(memStream);
                    var resultStr = Encoding.UTF8.GetString(memStream.ToArray());
                    System.Diagnostics.Debug.WriteLine($"===> {resultStr}");

                    if (resultStr.ToUpper().Contains("OK"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
                return true;
            }
            else
                return false;
        }
    }
}
