using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageService.Logging.Modal;

namespace ImageServiceDesktopApp.Model
{
    interface ILogModel: INotifyPropertyChanged
    {
        ObservableCollection<LogEntry> LogMessages { get; set; }
    }
}
