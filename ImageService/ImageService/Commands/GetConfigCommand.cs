using ImageService.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class GetConfigCommand : ICommand
    {
        public string Execute(string[] args, out bool result)
        {
            try
            {
                result = true;
                TcpStringArgs tcpStringArgs = new TcpStringArgs();
                tcpStringArgs.args = new string[5];

                tcpStringArgs.args[0] = ConfigurationManager.AppSettings.Get("OutputDir");
                tcpStringArgs.args[1] = ConfigurationManager.AppSettings.Get("SourceName");
                tcpStringArgs.args[2] = ConfigurationManager.AppSettings.Get("LogName");
                tcpStringArgs.args[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
                tcpStringArgs.args[4] = ConfigurationManager.AppSettings.Get("Handler");

                return JsonConvert.SerializeObject(tcpStringArgs);



            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }
    }
}
