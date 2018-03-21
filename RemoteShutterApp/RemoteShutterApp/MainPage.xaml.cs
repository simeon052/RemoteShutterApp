using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RemoteShutterApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            TakePictureButton.Clicked += async (s, e) =>
            {
                var udp = new Lib.UdpHandler("192.168.10.30", 8888);
                var sentbytes = await udp.SendAsync("s");

                System.Diagnostics.Debug.WriteLine($"{sentbytes} bytes are sent");
            };

        }
    }
}
