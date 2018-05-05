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
         public delegate void UpdateResponseArrieved(CommandRecievedEventArgs responseObj);
        public event UpdateResponseArrieved UpdateResponse;
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

                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                CommandRecievedEventArgs clientType = new CommandRecievedEventArgs(-1, null, "", ImageService.Infrastructure.Enums.ClientType.WriterAndReader);
                string clientTypeStr = JsonConvert.SerializeObject(clientType);
                writer.Write(clientTypeStr);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;

            }
        }

        public bool Start2()
        {
            try
            {
                bool result = true;
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                m_isStopped = false;

                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                CommandRecievedEventArgs clientType = new CommandRecievedEventArgs(-1, null, "", ImageService.Infrastructure.Enums.ClientType.Reader);
                string clientTypeStr = JsonConvert.SerializeObject(clientType);
                writer.Write(clientTypeStr);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;

            }
        }
        public CommandRecievedEventArgs SendCommand(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {

                //////?????
                //CommandRecievedEventArgs clientType = new CommandRecievedEventArgs(-1, null, "", ImageService.Infrastructure.Enums.ClientType.WriterAndReader);
                //string clientTypeStr = JsonConvert.SerializeObject(clientType);
                //writer.Write(clientTypeStr);

                // Send data to server
                Console.WriteLine($"Send {jsonCommand} to Server");
                //writer.AutoFlush = true;
                writer.Write(jsonCommand);
                // Get result from server
                string result = reader.ReadString();
                Console.WriteLine($"Recieve {result} from Server");
                //this.CloseClient();
                return JsonConvert.DeserializeObject<CommandRecievedEventArgs>(result);
            }
        }

        public void RecieveCommand()
        {
            new Task(() =>
            {
                while (!m_isStopped)
                {
                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    //////?????
                    //CommandRecievedEventArgs clientType = new CommandRecievedEventArgs(-1, null, "", ImageService.Infrastructure.Enums.ClientType.Reader);
                    //string clientTypeStr = JsonConvert.SerializeObject(clientType);
                    //writer.Write(clientTypeStr);

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
