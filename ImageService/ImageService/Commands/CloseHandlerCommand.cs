﻿using ImageService.Commands;
using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class CloseHandlerCommand : ICommand
    {
        private ImageServer m_imageServer;

        public CloseHandlerCommand(ImageServer imageServer)
        {
            this.m_imageServer = imageServer;
           
        }


        public string Execute(string[] args, out bool result)
        {
            try
            {
                result = true;
                if (args==null || args.Length == 0)
                {
                    throw new Exception("Invalid args for deleting handler");
                }
                string toBeDeletedHandler = args[0];
                string[] directories = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
                StringBuilder sbNewHandlers = new StringBuilder();
                for (int i = 0; i < directories.Length; i++)
                {
                    if (directories[i]!=toBeDeletedHandler)
                    {
                        sbNewHandlers.Append(directories[i] + ";");
                    }
                }
                string newHandlers = (sbNewHandlers.ToString()).TrimEnd(';');
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                // Add an Application Setting.
                config.AppSettings.Settings.Remove("Handler");
                config.AppSettings.Settings.Add("Handler", newHandlers);
                 // Save the configuration file.
    config.Save(ConfigurationSaveMode.Modified);
                 // Force a reload of a changed section.
    ConfigurationManager.RefreshSection("appSettings");




                //config.AppSettings.Settings["Handler"].Value = newHandlers;
                //config.Save(ConfigurationSaveMode.Modified);
                //todo: stop listen to this dir!!
                this.m_imageServer.CloseSpecipicHandler(toBeDeletedHandler);
                //todo: update other customers!!!!!!
                return string.Empty;
            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }
    }
}
