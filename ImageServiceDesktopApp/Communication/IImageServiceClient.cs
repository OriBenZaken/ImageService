using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp
{
    public delegate void UpdateResponseArrieved(CommandRecievedEventArgs responseObj);

    interface IImageServiceClient
    {
        bool Start();
        void SendCommand(CommandRecievedEventArgs commandRecievedEventArgs);
        void CloseClient();
        void RecieveCommand();
        event UpdateResponseArrieved UpdateResponse;
    }
}
