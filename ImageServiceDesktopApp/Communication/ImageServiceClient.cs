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
using ImageService.Infrastructure.Enums;
using System.Threading;

namespace ImageServiceDesktopApp
{
    class ImageServiceClient : IImageServiceClient
    {
        private TcpClient client;
        private bool m_isStopped;
        public delegate void UpdateResponseArrived(CommandRecievedEventArgs responseObj);

        public event ImageServiceDesktopApp.UpdateResponseArrived UpdateResponse;
        private static ImageServiceClient m_instance;
        private ImageServiceClient()
        {
            this.IsConnected = this.Start();
        }
        private static Mutex m_mutex = new Mutex();



        public bool IsConnected { get; set; }


        public static ImageServiceClient Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ImageServiceClient();
                }
                return m_instance;
            }
        }

        private bool Start()
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
                try
                {
                    string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
                    NetworkStream stream = client.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    // Send data to server
                    Console.WriteLine($"Send {jsonCommand} to Server");
                    m_mutex.WaitOne();
                    writer.Write(jsonCommand);
                    m_mutex.ReleaseMutex();
                }
                catch (Exception ex)
                {

                }
            }).Start();
        }

        public void RecieveCommand()
        {
            new Task(() =>
            {
                try
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
                }
                catch (Exception ex)
                {

                }
            }).Start();
        }

        public void CloseClient()
        {
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseClient, null, "");
            this.SendCommand(commandRecievedEventArgs);
            client.Close();
            this.m_isStopped = true;
        }
    }
}
