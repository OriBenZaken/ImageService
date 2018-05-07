using ImageService.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ImageService.Logging.Modal;
using ImageService.Controller;
using ImageService.Modal;
using Newtonsoft.Json;
using System.Threading;

namespace ImageService
{
    class ImageServiceSrv : IImageServiceSrv
    {
        ILoggingService Logging { get; set; }
        int Port { get; set; }
        TcpListener Listener { get; set; }
        IClientHandler Ch { get; set; }
        private List<TcpClient> clients = new List<TcpClient>();
        private static Mutex m_mutex = new Mutex();


        public ImageServiceSrv(int port, ILoggingService logging, IClientHandler ch)
        {
            this.Port = port;
            this.Logging = logging;
            //this.Logging.UpdateLogEntries += NotifyAboutNewLogEntry;
            this.Ch = ch;
            ClientHandler.Mutex = m_mutex;

        }
        public void Start()
        {
            try
            {
                IPEndPoint ep = new
                IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);
                Listener = new TcpListener(ep);

                Listener.Start();
                Logging.Log("Waiting for client connections...", MessageTypeEnum.INFO);
                Task task = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            TcpClient client = Listener.AcceptTcpClient();
                            Logging.Log("Got new connection", MessageTypeEnum.INFO);
                            clients.Add(client);
                            Ch.HandleClient(client,clients);
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                    }
                Logging.Log("Server stopped", MessageTypeEnum.INFO);
                });
                task.Start();
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);
            }
        }
        public void Stop()
        {
            Listener.Stop();
        }

        public void NotifyAllClientsAboutUpdate(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            try
            {
                List<TcpClient> copyClients = new List<TcpClient>(clients);
                foreach (TcpClient client in copyClients)
                {
                    new Task(() =>
                    {
                        try
                        {
                            NetworkStream stream = client.GetStream();
                            BinaryWriter writer = new BinaryWriter(stream);
                            string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
                            m_mutex.WaitOne();
                            writer.Write(jsonCommand);
                            m_mutex.ReleaseMutex();
                            // client.Close();
                        }
                        catch (Exception ex)
                        {
                            this.clients.Remove(client);
                        }

                    }).Start();
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);
            }
        }

        private void NotifyAboutNewLogEntry(CommandRecievedEventArgs updateObj)
        {
            NotifyAllClientsAboutUpdate(updateObj);
        }
    }

}
