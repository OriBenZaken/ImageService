﻿using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ImgServiceWebApplication.Models
{
    public class Config
    {
        public delegate void NotifyAboutChange();
        public event NotifyAboutChange Notify;

        //ctr
        public Config()
        {
            GuiClient = Communication.ImageServiceClient.Instance;
            GuiClient.RecieveCommand();
            GuiClient.UpdateResponse += UpdateResponse;
            SourceName = "";
            LogName = "";
            OutputDirectory = "";
            ThumbnailSize = 1;
            Handlers = new ObservableCollection<string>();
            Enabled = false;
            string[] arr = new string[5];
            CommandRecievedEventArgs request = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arr, "");
            GuiClient.SendCommand(request);
        }
        private static Communication.IImageServiceClient GuiClient;


        /// <summary>
        /// UpdateResponse function.
        /// updates the model when message recieved from srv.
        /// </summary>
        /// <param name="responseObj">the info came from srv</param>
        private void UpdateResponse(CommandRecievedEventArgs responseObj)
        {
            try
            {
                if (responseObj != null)
                {
                    switch (responseObj.CommandID)
                    {
                        case (int)CommandEnum.GetConfigCommand:
                            UpdateConfigurations(responseObj);
                            break;
                        case (int)CommandEnum.CloseHandler:
                            //CloseHandler(responseObj);
                            break;
                    }
                    //update controller
                    Notify?.Invoke();
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// UpdateConfigurations function.
        /// updates app config params.
        /// </summary>
        /// <param name="responseObj">the info came from srv</param>
        private void UpdateConfigurations(CommandRecievedEventArgs responseObj)
        {
            try
            {
                OutputDirectory = responseObj.Args[0];
                SourceName = responseObj.Args[1];
                LogName = responseObj.Args[2];
                int num;
                int.TryParse(responseObj.Args[3], out num);
                ThumbnailSize = num;
                string[] handlers = responseObj.Args[4].Split(';');
                foreach (string handler in handlers)
                {
                    Handlers.Add(handler);
                }
            }
            catch (Exception ex)
            {

            }
        }
        //members
        [Required]
        [DataType(DataType.Text)]
        public bool Enabled { get; set; }

        [Required]
        [Display(Name = "Output Directory")]
        public string OutputDirectory { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Source Name")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Log Name")]
        public string LogName { get; set; }

        [Required]
        [Display(Name = "Thumbnail Size")]
        public int ThumbnailSize { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Handlers")]
        public ObservableCollection<string> Handlers { get; set; }

    }
}