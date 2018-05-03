
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using ImageService.Controller;
using Newtonsoft.Json;
using ImageService.Server;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
using System.Configuration;

namespace ImageService
{
    class ClientHandler : IClientHandler
    {
        IImageController ImageController { get; set; }
        ImageServer ImageServer { get; set; }
        public ClientHandler(IImageController imageController, ImageServer imageServer)
        {
            this.ImageController = imageController;
            this.ImageServer = imageServer;

        }
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    string commandLine = reader.ReadString();
                    CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandLine);

                    Console.WriteLine("Got command: {0}", commandLine);
                    bool r;
                    string result = this.ImageController.ExecuteCommand((int)commandRecievedEventArgs.CommandID,
                        commandRecievedEventArgs.Args, out r);
                   // string result = handleCommand(commandRecievedEventArgs);
                    writer.Write(result);
                }
                client.Close();
            }).Start();


        }

        private string handleCommand(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            string result = string.Empty;
            switch (commandRecievedEventArgs.CommandID)
            {
                case (int)CommandEnum.CloseHandler:

                    break;
                case (int)CommandEnum.GetConfigCommand:
                    string[] args = new string[5];

                    args[0] = ConfigurationManager.AppSettings.Get("OutputDir");
                    args[1] = ConfigurationManager.AppSettings.Get("SourceName");
                    args[2] = ConfigurationManager.AppSettings.Get("LogName");
                    args[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
                    args[4] = ConfigurationManager.AppSettings.Get("Handler");
                    CommandRecievedEventArgs commandSendArgs = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand,args,"");
                    result =  JsonConvert.SerializeObject(commandSendArgs);

                    break;
                case (int)CommandEnum.LogCommand:

                    break;
            }
            return result;

        }
    }
}
