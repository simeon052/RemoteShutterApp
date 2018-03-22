using System;
using System.Threading.Tasks;

namespace RemoteShutterConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length >= 2)
            {

                var udp = new Lib.UdpHandler(args[0], 8888);
                var sentbytes = await udp.SendAsync("s");

                System.Diagnostics.Debug.WriteLine($"{sentbytes} bytes are sent");

                var result = await Lib.UdpHandler.ShootLumix(args[1]);
                Console.WriteLine($"Shoot!! {sentbytes} : {result}");
            }
            else
            {
                Console.WriteLine("done!!");
            }
        }
    }
}
