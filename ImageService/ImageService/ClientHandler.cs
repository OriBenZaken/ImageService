﻿
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
        ILoggingService Logging { get; set; }
        /// <summary>
        /// ClientHandler constructor.
        /// </summary>
        /// <param name="imageController">IImageController obj</param>
        /// <param name="logging">ILoggingService obj</param>
        public ClientHandler(IImageController imageController, ILoggingService logging)//, ImageServer imageServer)
        {
            this.ImageController = imageController;
            this.Logging = logging;

        }
        private bool m_isStopped = false;
        public static Mutex Mutex { get; set; }
        /// <summary>
        /// HandleClient function.
        /// handles the client-server communication.
        /// </summary>
        /// <param name="client">specified client</param>
        /// <param name="clients">list of all current clients</param>
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
                            Logging.Log("ClientHandler got command: " +commandLine, MessageTypeEnum.INFO);

                            CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandLine);
                            if (commandRecievedEventArgs.CommandID == (int)CommandEnum.CloseClient)
                            {
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

    }
}
