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

namespace ImageService.ImageServiceSrv
{
    class ImageServiceSrv
    {
        ILoggingService Logging { get; set; }
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        public ImageServiceSrv(int port, ILoggingService logging, IClientHandler ch)
        {
            this.port = port;
            this.Logging = logging;
            this.ch = ch;

        }
        public void Start()
        {
            try
            {
                IPEndPoint ep = new
                IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                listener = new TcpListener(ep);

                listener.Start();
                Logging.Log("Waiting for client connections...", MessageTypeEnum.INFO);
                Task task = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            TcpClient client = listener.AcceptTcpClient();
                            Logging.Log("Got new connection", MessageTypeEnum.INFO);
                            //todo: add task
                            ch.HandleClient(client);
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
            listener.Stop();
        }
    }

}
