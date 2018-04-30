using ImageService.ImageServiceSrv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using ImageService.Controller;
using Newtonsoft.Json;

namespace ImageService
{
    class ClientHandler : IClientHandler
    {
        IImageController ImageController { get; set; }
        public ClientHandler(IImageController imageController)
        {
            this.ImageController = imageController;
        }
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string commandLine = reader.ReadLine();
                    TcpStringArgs tcpStringArgs = JsonConvert.DeserializeObject<TcpStringArgs>(commandLine);

                    Console.WriteLine("Got command: {0}", commandLine);
                    bool r;
                    string result = this.ImageController.ExecuteCommand((int)tcpStringArgs.CommandID,
                        tcpStringArgs.args, out r);
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
    }
}
