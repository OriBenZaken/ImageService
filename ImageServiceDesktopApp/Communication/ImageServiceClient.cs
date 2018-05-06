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
using System.Windows.Threading;

namespace ImageServiceDesktopApp
{
    class ImageServiceClient : IImageServiceClient
    {
        private TcpClient client;
        private bool m_isStopped;
        public event UpdateResponseArrived UpdateResponse;
        public bool Start()
        {
            try
            {
                bool result = true;
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                m_isStopped = false;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;

            }
        }

      
        public void SendCommand(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            new Task(() =>
            {
                string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                // Send data to server
                Console.WriteLine($"Send {jsonCommand} to Server");
                writer.Write(jsonCommand);
            }).Start();
        }

        public void RecieveCommand()
        {
            new Task(() =>
            {
                while (!m_isStopped)
                {
                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    string response = reader.ReadString();
                    Console.WriteLine($"Recieve {response} from Server");
                    CommandRecievedEventArgs responseObj = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(response);
                    this.UpdateResponse?.Invoke(responseObj);
                }
            }).Start();
        }

        public void CloseClient()
        {
            client.Close();
            this.m_isStopped = true;
        }
    }
}
