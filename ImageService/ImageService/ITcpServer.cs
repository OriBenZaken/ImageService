using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    interface ITcpServer
    {
        /// <summary>
        /// Start function.
        /// lissten to new clients.
        /// </summary>
        void Start();
    }
}
