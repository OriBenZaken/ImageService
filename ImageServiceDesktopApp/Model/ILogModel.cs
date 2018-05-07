using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageService.Logging.Modal;
using ImageService.Logging;

namespace ImageServiceDesktopApp.Model
{   /// <summary>
/// Log model interface.
/// </summary>
    interface ILogModel: INotifyPropertyChanged
    {
        // List of all the event log entries.
        ObservableCollection<LogEntry> LogEntries { get; set; }

    }
}
