using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageServiceDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp.VM
{
    class LogVM : ILogVM
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ILogModel logModel = new LogModel();
        public ObservableCollection<LogEntry> VM_LogEntries
        {
            get { return this.logModel.LogEntries; }
            set => throw new NotImplementedException();
        }
    }
}
