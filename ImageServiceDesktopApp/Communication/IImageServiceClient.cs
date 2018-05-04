using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp
{
    interface IImageServiceClient
    {
        void Start();
        CommandRecievedEventArgs SendCommand(CommandRecievedEventArgs commandRecievedEventArgs);
        void CloseClient();
    }
}
