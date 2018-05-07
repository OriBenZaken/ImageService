using ImageService.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp.VM
{
    /// <summary>
    /// interface of Log View Model.
    /// </summary>
    interface ILogVM:INotifyPropertyChanged
    {
        // List of all the event log entries.
        ObservableCollection<LogEntry> VM_LogEntries { get; set; }
    }
}
