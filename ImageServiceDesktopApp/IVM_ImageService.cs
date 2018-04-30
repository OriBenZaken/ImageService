using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ImageServiceDesktopApp
{
    interface IVM_ImageService: INotifyPropertyChanged
    {
        IImageServiceClient ImageServiceClient { get; set; }
    }
}
