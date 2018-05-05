using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public delegate void NotifyAllClients(CommandRecievedEventArgs commandRecievedEventArgs);
        public static event NotifyAllClients NotifyAllHandlerRemoved;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        public event EventHandler<DirectoryCloseEventArgs> CloseServer;
        public IImageController Controller
        {
            get
            {
                return this.m_controller;
            }
        }
        public ILoggingService Logging
        {
            get
            {
                return this.m_logging;
            }
        }
        public Dictionary<string, IDirectoryHandler> Handlers { get; set; }
        #endregion
        /// <summary>
        /// ImageServer ctr.
        /// </summary>
        /// <param name="controller">IImageController obj</param>
        /// <param name="logging">ILoggingService obj</param>
        public ImageServer(IImageController controller, ILoggingService logging)
        {
            this.m_controller = controller;
            this.m_logging = logging;
            this.Handlers = new Dictionary<string, IDirectoryHandler>();
            string[] directories = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));

            foreach (string path in directories)
            {
                try
                {
                    this.CreateHandler(path);
                }
                catch (Exception ex)
                {
                    this.m_logging.Log("Error while creating handler for directory: " + path + " because:" + ex.ToString(), MessageTypeEnum.FAIL);
                }
            }
        }
        public static void PerformSomeEvent(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            NotifyAllHandlerRemoved.Invoke(commandRecievedEventArgs);
        }
        
            internal void CloseSpecipicHandler(string toBeDeletedHandler)
        {
            if (Handlers.ContainsKey(toBeDeletedHandler))
            {
                IDirectoryHandler handler = Handlers[toBeDeletedHandler];
                this.CloseServer -= handler.OnCloseHandler;
                handler.OnCloseHandler(this,null);
            }
           
        }

        /// <summary>
        /// CreateHandler function.
        /// </summary>
        /// <param name="path">the path the handler is on charge</param>
        private void CreateHandler(string path)
        {
            IDirectoryHandler handler = new DirectoyHandler(m_logging, m_controller, path);
            Handlers[path]=handler;
            CommandRecieved += handler.OnCommandRecieved;
            this.CloseServer += handler.OnCloseHandler;
            handler.StartHandleDirectory(path);
            this.m_logging.Log("Handler was created for directory: " + path, MessageTypeEnum.INFO);
        }
        /// <summary>
        /// OnCloseServer function.
        /// defines what happens when we close the server
        /// </summary>
        public void OnCloseServer()
        {
            try
            {
                m_logging.Log("Enter OnCloseServer", MessageTypeEnum.INFO);
                CloseServer?.Invoke(this, null);
                m_logging.Log("Leave OnCloseServer", MessageTypeEnum.INFO);
            }
            catch (Exception ex)
            {
                this.m_logging.Log("OnColeServer Exception: " + ex.ToString(),MessageTypeEnum.FAIL);
            }
        }
    }
}
