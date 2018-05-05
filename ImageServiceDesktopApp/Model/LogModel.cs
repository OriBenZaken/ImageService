using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;
using Newtonsoft.Json;

namespace ImageServiceDesktopApp.Model
{
    class LogModel : ILogModel
    {
        private ObservableCollection<LogEntry> logEntries;
        public IImageServiceClient ImageServiceClient { get; set; }
        public LogModel()
        {
            this.ImageServiceClient = new ImageServiceClient();
            this.ImageServiceClient.Start();
            this.ImageServiceClient.RecieveCommand();
            this.ImageServiceClient.UpdateResponse += UpdateResponse;
            this.InitializeLogsParams();
        }
        public ObservableCollection<LogEntry> LogEntries
        {
            get
            {
                return this.logEntries;
            }
            set => throw new NotImplementedException();
        }

        public bool IsConected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;

        //for testing
        private void CreateLogExamples(int numOfExamples)
        {
            string[] logTypes = { "INFO", "WARNING", "FAIL" };
            Random rnd = new Random();
            for (int i = 0; i < numOfExamples; i++)
            {
                int type = rnd.Next(3); // creates a number between 0 and 2
                this.logEntries.Add(new LogEntry { Type = logTypes[type], Message = "Example " + i.ToString() });
            }
        }

        private void InitializeLogsParams()
        {
            this.logEntries = new ObservableCollection<LogEntry>();
            Object thisLock = new Object();
            BindingOperations.EnableCollectionSynchronization(LogEntries, thisLock);
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
            this.ImageServiceClient.SendCommand(commandRecievedEventArgs);
        }

        private void UpdateResponse(CommandRecievedEventArgs responseObj)
        {
            if (responseObj != null)
            {
                switch(responseObj.CommandID)
                {
                    case (int)CommandEnum.LogCommand:
                        IntializeLogEntriesList(responseObj);
                        break;
                    default: break;
                }
            }
        }

        private void IntializeLogEntriesList(CommandRecievedEventArgs responseObj)
        {
            try
            {
                foreach (LogEntry log in JsonConvert.DeserializeObject<ObservableCollection<LogEntry>>(responseObj.Args[0]))
                {
                    this.LogEntries.Add(log);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddLogEntry(CommandRecievedEventArgs responseObj)
        {
            try
            {
                LogEntry newLogEntry = JsonConvert.DeserializeObject<LogEntry>(responseObj.Args[0]);
                this.LogEntries.Insert(0, newLogEntry);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
