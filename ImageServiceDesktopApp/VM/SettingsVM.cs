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
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            //model.PropertyChanged += PropertyChanged1;
           
        }
        //private void PropertyChanged1(object sender, PropertyChangedEventArgs e)
        //{
        //    var command = this.RemoveCommand as DelegateCommand<object>;
        //    command.RaiseCanExecuteChanged();
        //}

        private void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propName);
            this.PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }
        public ObservableCollection<string> VM_Handlers
        {
            get { return model.Handlers; }
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
            string[] arr = { this.selectedItem };
            CommandRecievedEventArgs eventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandler, arr,"");
            //todo: send the command via tcp
            CommandRecievedEventArgs result = this.ImageServiceClient.SendCommand(eventArgs);
            

            this.model.Handlers.Remove(selectedItem);
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
