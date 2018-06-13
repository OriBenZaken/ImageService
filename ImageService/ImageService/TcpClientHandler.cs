﻿using ImageService.Controller;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService
{
    class TcpClientHandler:ITcpClientHandler
    {
        IImageController ImageController { get; set; }
        ILoggingService Logging { get; set; }
        /// <summary>
        /// ClientHandler constructor.
        /// </summary>
        /// <param name="imageController">IImageController obj</param>
        /// <param name="logging">ILoggingService obj</param>
        public TcpClientHandler(IImageController imageController, ILoggingService logging)//, ImageServer imageServer)
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
                            Byte[] data = new Byte[2048];
                            stream.Read(data, 0, data.Length);
                            //BinaryReader reader = new BinaryReader(stream);
                            //BinaryWriter writer = new BinaryWriter(stream);
                            //string commandLine = reader.ReadString();
                            //Logging.Log("ClientHandler got command: " + commandLine, MessageTypeEnum.INFO);


                            
                               // Image img = Image.FromStream(data);
                        
                       // CommandRecievedEventArgs commandRecievedEventArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandLine);
                            //if (commandRecievedEventArgs.CommandID == (int)CommandEnum.CloseClient)
                            //{
                            //    clients.Remove(client);
                            //    client.Close();
                            //    break;

                            //}
                           // Console.WriteLine("Got command: {0}", commandLine);
                            bool r;
                            //string result = this.ImageController.ExecuteCommand((int)commandRecievedEventArgs.CommandID,
                            //    commandRecievedEventArgs.Args, out r);
                            Mutex.WaitOne();
                           // writer.Write(result);
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
