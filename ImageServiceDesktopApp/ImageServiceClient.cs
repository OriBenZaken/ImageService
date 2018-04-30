using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using ImageService.Logging;
using ImageService.Logging.Modal;

namespace ImageServiceDesktopApp
{
    class ImageServiceClient
    {
        ILoggingService Logging { get; set; }
        public ImageServiceClient(ILoggingService logging)
        {
            Logging = logging;
        }
        public void Start()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine();
                Logging.Log("You are connected", MessageTypeEnum.INFO);
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Send data to server
                    int command = int.Parse(Console.ReadLine());
                    Logging.Log($"Send {command} to Server", MessageTypeEnum.INFO);
                    // Get result from server
                    int result = reader.ReadInt32();
                    Logging.Log($"Recieve {result} from Server", MessageTypeEnum.INFO);
                }
                client.Close();
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);
            }
        }
    }
}
