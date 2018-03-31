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
using ImageService.Server;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        private readonly string[] validExtensions = { ".jpg", ".png", ".gif", ".bmp" };
        #endregion




        public DirectoyHandler(ILoggingService logging, IImageController controller, string path)
        {
            this.m_logging = logging;
            this.m_controller = controller;
            this.m_path = path;
            this.m_dirWatcher = new FileSystemWatcher(this.m_path);
        }

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool result;
            // if (e.Args != null && e.Args.Length > 0 && this.m_path.StartsWith(e.Args[0]))
            //  {
            this.m_logging.Log("entered OnCommandRecieved: commandID:" + e.CommandID.ToString() + " path:" + e.Args[0], MessageTypeEnum.FAIL);
            string msg = this.m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
            // write result msg to the event long.
            if (result)
            {
                this.m_logging.Log(msg, MessageTypeEnum.INFO);
            }
            else
            {
                this.m_logging.Log(msg, MessageTypeEnum.FAIL);
            }
            //  }

        }

        public void StartHandleDirectory(string dirPath)
        {
            m_logging.Log("enter StartHandleDirectory" + " " + dirPath, MessageTypeEnum.FAIL);
            string[] filesInDirectory = Directory.GetFiles(m_path);
            foreach (string filepath in filesInDirectory)
            {
                m_logging.Log("StartHandleDirectory" + " " + filepath, MessageTypeEnum.FAIL);
                string extension = Path.GetExtension(filepath);
                if (this.validExtensions.Contains(extension))
                {
                    string[] args = { filepath };
                    OnCommandRecieved(this, new CommandRecievedEventArgs(1, args, filepath));
                }
            }
            this.m_dirWatcher.Created += new FileSystemEventHandler(M_dirWatcher_Created);
            this.m_dirWatcher.Changed += new FileSystemEventHandler(M_dirWatcher_Created);
            //start listen to directory
            this.m_dirWatcher.EnableRaisingEvents = true;
            this.m_logging.Log("Start handle directory: " + dirPath, MessageTypeEnum.INFO);

        }

        private void M_dirWatcher_Created(object sender, FileSystemEventArgs e)
        {
            this.m_logging.Log("Enterd M_durWatcher_Created with: " + e.FullPath, MessageTypeEnum.INFO);
            string extension = Path.GetExtension(e.FullPath);
            if (this.validExtensions.Contains(extension))
            {
                this.m_logging.Log("Enterd  if (this.validExtensions.Contains(extension)) with: " + e.FullPath, MessageTypeEnum.FAIL);
                string[] args = { e.FullPath };

                //todo: check which path to pass
                CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.NewFileCommand, args, "");
                this.OnCommandRecieved(this, commandRecievedEventArgs);
            }


        }

        public void OnCloseHandler(object sender, DirectoryCloseEventArgs e)
        {
            try
            {
                this.m_dirWatcher.EnableRaisingEvents = false;
                ((ImageServer)sender).CommandRecieved -= this.OnCommandRecieved;
                this.m_logging.Log("Succsess on closing handler of path " + this.m_path, MessageTypeEnum.INFO);
            }
            catch (Exception ex)
            {
                this.m_logging.Log("Error while trying to close handler of path "+this.m_path+ " "
                    + ex.ToString(), MessageTypeEnum.FAIL);
            }
        }

    }
}
