using ImageServiceDesktopApp.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageServiceDesktopApp.VM
{
    class MainWindowVM : IMainWindowVM
    {
        private IMainWindowModel m_mainWindowModel;
        public bool VM_IsConnected
        {
            get
            {
                return this.m_mainWindowModel.IsConnected;
            }
        }

        public ICommand CloseCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// MainWindowVM constructor.
        /// </summary>
        public MainWindowVM()
        {
            this.m_mainWindowModel = new MainWindowModel();
            this.m_mainWindowModel.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            this.CloseCommand = new DelegateCommand<object>(this.OnClose, this.CanClose);
        }
        /// <summary>
        /// OnClose function. 
        /// defines what happens before the window is closed.
        /// </summary>
        /// <param name="obj"></param>
        private void OnClose(object obj)
        {
            this.m_mainWindowModel.GuiClient.CloseClient();
        }
        /// <summary>
        /// CanClose function.
        /// defines if user can close the window.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanClose(object obj)
        {
            return true;
        }

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

    }
}
