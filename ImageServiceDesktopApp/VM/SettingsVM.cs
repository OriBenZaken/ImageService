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
    class SettingsVM : ISettingsVM
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ISettingModel model;
        public SettingsVM(ISettingModel model)
        {
            this.model = model;
            model.PropertyChanged +=
 delegate (Object sender, PropertyChangedEventArgs e)
 {
     NotifyPropertyChanged("VM_" + e.PropertyName);
     
 };            vm_handlers = new ObservableCollection<string>();
        }

        private void NotifyPropertyChanged(string propName)
        {
            throw new NotImplementedException();
        }
        private ObservableCollection<string> vm_handlers;
        public ObservableCollection<string> VM_Handlers
        {
            get
            {
                for (int i = 0; i < 10; i++)
                {
                    vm_handlers.Add(i.ToString());
                }
                return vm_handlers;
            }
            set
            {
                vm_handlers = value;
            }
        }

    }
}
