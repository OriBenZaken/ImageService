using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
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
                string jsonLogMessages = JsonConvert.SerializeObject(logMessages);
                string[] arr = new string[1];
                arr[0] = jsonLogMessages;
                CommandRecievedEventArgs commandSendArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, arr, "");
                result = true;
                return JsonConvert.SerializeObject(commandSendArgs);
            } catch (Exception e)
            {
                result = false;
                return "LogCommand.Execute: Failed execute log command";
            }
        }
    }
}
