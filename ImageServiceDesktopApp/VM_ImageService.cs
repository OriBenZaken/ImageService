using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp
{
    class VM_ImageService : IVM_ImageService
    {
        public IImageServiceClient ImageServiceClient { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
