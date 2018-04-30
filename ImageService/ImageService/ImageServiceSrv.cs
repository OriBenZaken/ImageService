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

namespace ImageService.ImageServiceSrv
{
    class ImageServiceSrv
    {
        ILoggingService Logging { get; set; }
        int Port { get; set; }
        TcpListener Listener { get; set; }
        IClientHandler Ch { get; set; }

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
                            Logging.Log("Got new connection", MessageTypeEnum.INFO);
                            //todo: add task
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
    }

}
