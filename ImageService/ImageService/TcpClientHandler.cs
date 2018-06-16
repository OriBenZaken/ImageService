using ImageService.Controller;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
                        Logging.Log("Start transfer photos!", MessageTypeEnum.INFO);
                        NetworkStream stream = client.GetStream();
                        //get the image name
                        string finalNameString = GetFileName(stream);
                        //tell the client we got the name 
                        Byte[] confirmation = new byte[1];
                        confirmation[0] = 1;
                        stream.Write(confirmation, 0, 1);
                        //read the image
                        List<Byte> finalbytes = GetImageBytes(stream);
                        //save the image
                        File.WriteAllBytes(ImageController.ImageServer.Directories[0] + @"\" + finalNameString + ".jpg", finalbytes.ToArray());
                        System.Threading.Thread.Sleep(500);
                        //client.Close();
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
        private string  GetFileName(NetworkStream stream)
        {
            Byte[] temp = new Byte[1];
            List<Byte> finalName = new List<byte>();
            //read the file name
            do
            {
                stream.Read(temp, 0, 1);
                finalName.Add(temp[0]);
            } while (stream.DataAvailable);

            return Path.GetFileNameWithoutExtension(System.Text.Encoding.UTF8.GetString(finalName.ToArray()));

        }

        private List<Byte> GetImageBytes(NetworkStream stream)
        {
            List<Byte> finalbytes = new List<byte>();
            Byte[] tempForReadBytes;
            Byte[] data = new Byte[6790];
            int i = 0;

            do
            {
                i = stream.Read(data, 0, data.Length);
                tempForReadBytes = new byte[i];
                for (int n = 0; n < i; n++)
                {
                    tempForReadBytes[n] = data[n];
                    finalbytes.Add(tempForReadBytes[n]);

                }
                System.Threading.Thread.Sleep(300);
            } while (stream.DataAvailable || i == data.Length);
            return finalbytes;
        }


    }
}
