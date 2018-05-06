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
using System.Windows.Data;

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
            this.ImageServiceClient = ImageServiceClient.Instance;
            this.ImageServiceClient.RecieveCommand();
            this.ImageServiceClient.UpdateResponse += UpdateResponse;
            this.InitializeSettingsParams();


        }
        private void UpdateResponse(CommandRecievedEventArgs responseObj)
        {
            try
            {
                if (responseObj != null)
                {
                    switch (responseObj.CommandID)
                    {
                        case (int)CommandEnum.GetConfigCommand:
                            this.OutputDirectory = responseObj.Args[0];
                            this.SourceName = responseObj.Args[1];
                            this.LogName = responseObj.Args[2];
                            this.TumbnailSize = responseObj.Args[3];
                            string[] handlers = responseObj.Args[4].Split(';');
                            foreach (string handler in handlers)
                            {
                                this.Handlers.Add(handler);
                            }
                            break;
                        case (int)CommandEnum.CloseHandler:
                            if (Handlers != null && Handlers.Count > 0 && responseObj != null && responseObj.Args != null
                                  && Handlers.Contains(responseObj.Args[0]))
                            {
                                this.Handlers.Remove(responseObj.Args[0]);
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void InitializeSettingsParams()
        {
            try
            {
                this.OutputDirectory = string.Empty;
                this.SourceName = string.Empty;
                this.LogName = string.Empty;
                this.TumbnailSize = string.Empty;
                Handlers = new ObservableCollection<string>();
                Object thisLock = new Object();
                BindingOperations.EnableCollectionSynchronization(Handlers, thisLock);
                CommandRecievedEventArgs request = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, "");
                this.ImageServiceClient.SendCommand(request);
            }
            catch (Exception ex)
            {

            }
            

        }
        public ImageServiceClient ImageServiceClient { get; set; }

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
        public bool IsConected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
