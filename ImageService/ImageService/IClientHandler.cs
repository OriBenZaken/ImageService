using System.Collections.Generic;
using System.Net.Sockets;

namespace ImageService
{
    internal interface IClientHandler
    {
        /// <summary>
        /// HandleClient function.
        /// handles the client-server communication.
        /// </summary>
        /// <param name="client">specified client</param>
        /// <param name="clients">list of all current clients</param>
        void HandleClient(TcpClient client, List<TcpClient> clients);
    }
}