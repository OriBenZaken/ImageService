using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp.Model
{
    class SettingModel : ISettingModel
    {
        #region Notify Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        public SettingModel()
        {
            this.imageServiceClient = new ImageServiceClient();
            bool rc = this.imageServiceClient.Start();
            if (!rc)
            {
                this.IsConected = false;
            }
            this.InitializeSettingsParams();


        }
        private void InitializeSettingsParams()
        {
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, "");
            CommandRecievedEventArgs result = this.imageServiceClient.SendCommand(commandRecievedEventArgs);
            this.OutputDirectory = result.Args[0];
            this.SourceName = result.Args[1];
            this.LogName = result.Args[2];
            this.TumbnailSize = result.Args[3];
            Handlers = new ObservableCollection<string>();
            string[] handlers = result.Args[4].Split(';');
            foreach (string handler in handlers)
            {
                this.Handlers.Add(handler);
            }
        }
        private ImageServiceClient imageServiceClient;
        private string m_outputDirectory;
        public string OutputDirectory
        {
            get { return m_outputDirectory; }
            set
            {
                m_outputDirectory = value;
                OnPropertyChanged("OutputDirectory");
            }
        }
        private string m_sourceName;
        public string SourceName
        {
            get { return m_sourceName; }
            set
            {
                m_sourceName = value;
                OnPropertyChanged("SourceName");
            }
        }
        private string m_logName;
        public string LogName
        {
            get { return m_logName; }
            set
            {
                m_logName = value;
                OnPropertyChanged("LogName");
            }
        }
        private string m_tumbnailSize;
        public string TumbnailSize
        {
            get { return m_tumbnailSize; }
            set
            {
                m_tumbnailSize = value;
                OnPropertyChanged("TumbnailSize");
            }
        }
        public ObservableCollection<string> Handlers { get; set; }
        private bool m_isConected;
        public bool IsConected
        {
            get { return m_isConected; }
            set
            {
                m_isConected = value;
                OnPropertyChanged("IsConected");
            }
        }
    }
}
