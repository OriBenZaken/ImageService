using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        private readonly string[] validExtensions = { "jpg", "png", "gif", "bmp" };
        #endregion




        public DirectoyHandler(ILoggingService m_logging, IImageController m_controller, string path)
        {
            this.m_logging = m_logging;
            this.m_controller = m_controller;
            this.m_path = path;
            this.m_dirWatcher = new FileSystemWatcher(this.m_path);
        }

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool result;
            string msg  = this.m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
            // write result msg to the event long.
           if (result)
            {
                this.m_logging.Log(msg, MessageTypeEnum.INFO);
            } else
            {
                this.m_logging.Log(msg, MessageTypeEnum.FAIL);
            }
        }

        public void StartHandleDirectory(string dirPath)
        {
            this.m_logging.Log("Start handle directory: " + dirPath, MessageTypeEnum.INFO);
            this.m_dirWatcher.Created += new FileSystemEventHandler(M_dirWatcher_Created);
            // start listen to directory
            this.m_dirWatcher.EnableRaisingEvents = true;
        }

        private void M_dirWatcher_Created(object sender, FileSystemEventArgs e)
        {
            string extension = Path.GetExtension(e.FullPath);
            if (this.validExtensions.Contains(extension))
            {
                string[] args = { e.FullPath };
                //todo: check which path to pass
                CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs(1,args , "");
                this.OnCommandRecieved(this, commandRecievedEventArgs);
            }


    }

    }
}
