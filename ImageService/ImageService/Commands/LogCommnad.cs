using ImageService.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{

    class LogCommand : ICommand
    {
        private ILoggingService loggingService;
        public LogCommand(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }
        public string Execute(string[] args, out bool result)
        {
            try
            {
                ObservableCollection<LogEntry> logMessages = this.loggingService.LogMessages;
                string json = JsonConvert.SerializeObject(logMessages);
                result = true;
                return json;
            } catch (Exception e)
            {
                result = false;
                return "LogCommand.Execute: Failed execute log command";
            }
        }
    }
}
