using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using ImageServiceDesktopApp.Model;
using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

 };
            vm_handlers = new ObservableCollection<string>();
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            model.PropertyChanged += PropertyChanged1;
        }
        private void PropertyChanged1(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
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
        public ICommand RemoveCommand;
        private void OnRemove(object obj)
        {
            ListBox listBox= ((ListBox)(obj));
            string toBeDeletedHandler = listBox.SelectedItem.ToString();
            string[] arr = { toBeDeletedHandler };
            CommandRecievedEventArgs eventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandler, arr,"");
            string message = JsonConvert.SerializeObject(eventArgs);
            //todo: send the command via tcp
        }

        private bool CanRemove(object obj)
        {
            return false;
        }


    }
}
