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

namespace ImageService
{
    class ImageServiceSrv : IImageServiceSrv
    {
        ILoggingService Logging { get; set; }
        int Port { get; set; }
        TcpListener Listener { get; set; }
        IClientHandler Ch { get; set; }
        private List<TcpClient> clients = new List<TcpClient>();

        public ImageServiceSrv(int port, ILoggingService logging, IClientHandler ch)
        {
            this.Port = port;
            this.Logging = logging;
            this.Ch = ch;

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
                            clients.Add(client);
                            Logging.Log("Got new connection", MessageTypeEnum.INFO);
                            Ch.HandleClient(client);
                        }
                        catch (SocketException)
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

            }
            catch(Exception ex)
            {

            }
        }
    }

}
