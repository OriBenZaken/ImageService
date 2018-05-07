using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    interface IImageServiceSrv
    {
        /// <summary>
        /// Start function.
        /// lissten to new clients.
        /// </summary>
        void Start();
        /// <summary>
        /// Stop func.
        /// stop listen to new clients.
        /// </summary>
        void Stop();
        /// <summary>
        /// NotifyAllClientsAboutUpdate function.
        /// notifies all clients about update (new log, handler deleted).
        /// </summary>
        /// <param name="commandRecievedEventArgs"></param>
        void NotifyAllClientsAboutUpdate(CommandRecievedEventArgs commandRecievedEventArgs);

    }

}
