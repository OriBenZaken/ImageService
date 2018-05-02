using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging.Modal;

namespace ImageServiceDesktopApp.Model
{
    class LogModel : ILogModel
    {
        private ObservableCollection<LogEntry> logMessages;
        public LogModel()
        {
            this.logMessages = new ObservableCollection<LogEntry>();
            this.logMessages.Add(new LogEntry { Type = "INFO", Message = "Hello! 1" });
            this.logMessages.Add(new LogEntry { Type = "FAIL", Message = "Hello! 2" });
            this.logMessages.Add(new LogEntry { Type = "WARNING", Message = "Hello! 3" });
            this.logMessages.Add(new LogEntry { Type = "FAIL", Message = "Hello! 4" });
            this.logMessages.Add(new LogEntry { Type = "WARNING", Message = "Hello! 5" });
            this.logMessages.Add(new LogEntry { Type = "INFO", Message = "Hello! 6" });

        }
        public ObservableCollection<LogEntry> LogMessages {
            get
            {
                return this.logMessages;
            }
            set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
