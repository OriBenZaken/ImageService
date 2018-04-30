using System.Net.Sockets;

namespace ImageService.ImageServiceSrv
{
    internal interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}