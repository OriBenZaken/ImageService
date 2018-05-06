using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using ImageServiceDesktopApp.Model;
using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImageServiceDesktopApp.VM
{
    class SettingsVM : ISettingsVM
    {
        /// <summary>
        /// SettingsVM constructor.
        /// </summary>
        public SettingsVM()
        {
            this.model = new SettingModel();
            model.PropertyChanged +=
 delegate (Object sender, PropertyChangedEventArgs e)
 {
     NotifyPropertyChanged("VM_" + e.PropertyName);

 };
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);

        }

        #region MVVMLogic
        public event PropertyChangedEventHandler PropertyChanged;
        private ISettingModel model;
        /// <summary>
        /// NotifyPropertyChanged function.
        /// invokes PropertyChanged event about change of property.
        /// </summary>
        /// <param name="propName">the changed property</param>
        private void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propName);
            this.PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }
        //getters and setters
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
        #endregion
        #region CommandsLogic
        public ICommand RemoveCommand { get; set; }
        /// <summary>
        /// OnRemove function.
        /// tells what will happen when we press Remove button.
        /// </summary>
        /// <param name="obj"></param>
        private void OnRemove(object obj)
        {
            try
            {
                string[] arr = { this.selectedItem };
                CommandRecievedEventArgs eventArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseHandler, arr, "");
                this.model.ImageServiceClient.SendCommand(eventArgs);
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// CanRemove function.
        /// sets the enabeld of remove button.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanRemove(object obj)
        {
            bool result = this.selectedItem != null ? true : false;
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
        #endregion
    }
}
