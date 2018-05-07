
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
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Threading;

namespace ImageService
{
    class ClientHandler : IClientHandler
    {
        IImageController ImageController { get; set; }
       // ImageServer ImageServer { get; set; }
        ILoggingService Logging { get; set; }
        public ClientHandler(IImageController imageController, ILoggingService logging)//, ImageServer imageServer)
        {
            this.ImageController = imageController;
            this.Logging = logging;

        }
        private bool m_isStopped = false;
        public static Mutex Mutex { get; set; }

        public void HandleClient(TcpClient client, List<TcpClient> clients)
        {
            try
            {

                new Task(() =>
                {
                    try
                    {
                        while (!m_isStopped)
                        {

                            NetworkStream stream = client.GetStream();
                            BinaryReader reader = new BinaryReader(stream);
                            BinaryWriter writer = new BinaryWriter(stream);
                            string commandLine = reader.ReadString();
                            Logging.Log(commandLine, MessageTypeEnum.FAIL);

                            CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandLine);
                            if (commandRecievedEventArgs.CommandID == (int)CommandEnum.CloseClient)
                            {
                              // m_isStopped = true;
                                clients.Remove(client);
                                client.Close();
                                break;

                            }
                            Console.WriteLine("Got command: {0}", commandLine);
                            bool r;
                            string result = this.ImageController.ExecuteCommand((int)commandRecievedEventArgs.CommandID,
                                commandRecievedEventArgs.Args, out r);
                            // string result = handleCommand(commandRecievedEventArgs);
                            Mutex.WaitOne();

                            writer.Write(result);
                            Mutex.ReleaseMutex();
                        

                        }
                    }
                    catch (Exception ex)
                    {
                        //m_isStopped = true;
                        clients.Remove(client);
                        Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);
                        client.Close();
                    }

                }).Start();
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);

            }


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
