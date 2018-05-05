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
        bool Start();
        bool Start2();

        CommandRecievedEventArgs SendCommand(CommandRecievedEventArgs commandRecievedEventArgs);
        void CloseClient();
        void RecieveCommand();
    }
}
