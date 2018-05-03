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
        public IImageServiceClient ImageServiceClient { get; set; }

        public SettingsVM()
        {
            this.model = new SettingModel();
            model.PropertyChanged +=
 delegate (Object sender, PropertyChangedEventArgs e)
 {
     NotifyPropertyChanged("VM_" + e.PropertyName);

 };
            vm_handlers = new ObservableCollection<string>();
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            model.PropertyChanged += PropertyChanged1;
            this.ImageServiceClient = new ImageServiceClient();
            this.ImageServiceClient.Start();
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand,null,"");
            CommandRecievedEventArgs result = ImageServiceClient.SendCommand(JsonConvert.SerializeObject(commandRecievedEventArgs));
            this.model.OutputDirectory = result.Args[0];
            this.model.SourceName = result.Args[1];
            this.model.LogName = result.Args[2];
            this.model.TumbnailSize = result.Args[3];
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
        public string VM_OutputDirectory
        {
            get { return model.OutputDirectory; }
        }
        public string VM_SourceName
        {
            get { return model.SourceName; }
        }
        public string VM_LogName
        {
            get { return model.LogName; }
        }
        public string VM_TumbnailSize
        {
            get { return model.TumbnailSize; }
        }

      

        public ICommand RemoveCommand { get; set; }

        private void OnRemove(object obj)
        {
            //ListBox listBox= ((ListBox)(obj));
            //string toBeDeletedHandler = listBox.SelectedItem.ToString();
            string[] arr = { this.selectedItem };
            CommandRecievedEventArgs eventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandler, arr,"");
            string message = JsonConvert.SerializeObject(eventArgs);
            //todo: send the command via tcp
            CommandRecievedEventArgs result = this.ImageServiceClient.SendCommand(message);
            

            this.vm_handlers.Remove(selectedItem);
        }

        private bool CanRemove(object obj)
        {
            bool result =  this.selectedItem != null ? true : false;
            return result;
        }
        private string selectedItem;
        public string SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                selectedItem = value;
                var command = this.RemoveCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
        }


    }
}
