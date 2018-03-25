using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
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
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion

        private void CreateHandler(string path)
        {
            IDirectoryHandler handler = new DirectoyHandler(m_logging, m_controller,path );
            CommandRecieved += handler.OnCommandRecieved;
            handler.DirectoryClose += onCloseHandler;
        }


        public void sendCommand(CommandRecievedEventArgs args)
        {
            CommandRecieved?.Invoke(this, args);
        }

        public void onCloseHandler(object sender, DirectoryCloseEventArgs args)
        {
            m_logging.Log(args.Message, Logging.Modal.MessageTypeEnum.INFO);
            IDirectoryHandler handler = (IDirectoryHandler)sender;
            CommandRecieved -= handler.OnCommandRecieved;
        }

    }
}
