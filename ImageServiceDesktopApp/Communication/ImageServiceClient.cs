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
using ImageService.Modal;
using Newtonsoft.Json;

namespace ImageServiceDesktopApp
{
    class ImageServiceClient : IImageServiceClient
    {
        private TcpClient client;
        public void Start()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public CommandRecievedEventArgs SendCommand(string jsonCommand)
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.WriteLine($"Send {jsonCommand} to Server");
                //writer.AutoFlush = true;
                writer.Write(jsonCommand);
                // Get result from server
                string result = reader.ReadString();
                Console.WriteLine($"Recieve {result} from Server");
                return JsonConvert.DeserializeObject<CommandRecievedEventArgs>(result);
            }
        }
        public void CloseClient()
        {
            client.Close();
        }
    }
}
