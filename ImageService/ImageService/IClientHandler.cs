using System.Collections.Generic;
using System.Net.Sockets;

namespace ImageService
{
    internal interface IClientHandler
    {
        void HandleClient(TcpClient client, List<TcpClient> clients);
    }
}