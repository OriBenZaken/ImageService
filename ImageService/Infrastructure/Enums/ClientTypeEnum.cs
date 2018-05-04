using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infrastructure.Enums
{
    public enum ClientType : int
    {
        NewFileCommand = 1,
        CloseCommand,
        GetConfigCommand,
        LogCommand,
        CloseHandler

    }
}
