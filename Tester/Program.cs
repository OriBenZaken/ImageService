using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageServiceModal imageServiceModal = new ImageServiceModal()
            {
                OutputFolder = @"C:\Users\lizah\Desktop\OutputFiles",
                ThumbnailSize = 1
            };
            bool result;
            string error = imageServiceModal.AddFile(@"C:\Users\lizah\Desktop\sourceFiles\liz.jpeg", out result);

        }
    }
}
