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
        void Start();
        void Stop();
        void NotifyAllClientsAboutUpdate(CommandRecievedEventArgs commandRecievedEventArgs);

    }
}
