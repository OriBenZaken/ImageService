using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImageServiceDesktopApp.VM
{
    interface ISettingsVM:INotifyPropertyChanged
    {
        ObservableCollection<string> VM_Handlers { get; set; }
    }
}
