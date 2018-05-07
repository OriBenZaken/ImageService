using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp.Model
{
    class MainWindowModel : IMainWindowModel
    {
        private bool m_isConnected;
        public bool IsConnected
        {
            get { return m_isConnected; }
            set
            {
                m_isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }

        public IImageServiceClient GuiClient { get; set; }

        public MainWindowModel()
        {
            GuiClient = ImageServiceClient.Instance;
            IsConnected = GuiClient.IsConnected;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
