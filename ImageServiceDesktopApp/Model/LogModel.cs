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
    /// <summary>
    /// Implementation of ILogModel interface.
    /// </summary>
    class LogModel : ILogModel
    {
        // List of all the event log entries.
        private ObservableCollection<LogEntry> logEntries;
        public IImageServiceClient GuiClient { get; set; }
        // Property - List of all the event log entries. 
        public ObservableCollection<LogEntry> LogEntries
        {
            get
            {
                return this.logEntries;
            }
            set => throw new NotImplementedException();
        }

        // boolean property, represents if the model is connected to the image service.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Log model constructor.
        /// </summary>
        public LogModel()
        {
            this.GuiClient = ImageServiceClient.Instance;
            this.GuiClient.UpdateResponse += UpdateResponse;
            this.InitializeLogsParams();
        }

        /// <summary>
        /// retreive event log entries list from the image service.
        /// </summary>
        private void InitializeLogsParams()
        {
            this.logEntries = new ObservableCollection<LogEntry>();
            Object thisLock = new Object();
            BindingOperations.EnableCollectionSynchronization(LogEntries, thisLock);
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
            this.GuiClient.SendCommand(commandRecievedEventArgs);
        }

        /// <summary>
        /// get CommandRecievedEventArgs object which was sent from the image service.
        /// reacts only if the commandID is relevant to the log model.
        /// </summary>
        /// <param name="responseObj"></param>
        private void UpdateResponse(CommandRecievedEventArgs responseObj)
        {
            if (responseObj != null)
            {
                switch(responseObj.CommandID)
                {
                    case (int)CommandEnum.LogCommand:
                        IntializeLogEntriesList(responseObj);
                        break;
                    case (int)CommandEnum.AddLogEntry:
                        AddLogEntry(responseObj);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Initialize log event entries list.
        /// </summary>
        /// <param name="responseObj">expected json string of ObservableCollection<LogEntry> in responseObj.Args[0]</param>
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

        /// <summary>
        /// adds new log entry to the event log entries list
        /// </summary>
        /// <param name="responseObj">expected responseObj.Args[0] = EntryType,  responseObj.Args[1] = Message </param>
        private void AddLogEntry(CommandRecievedEventArgs responseObj)
        {
            try
            {
                LogEntry newLogEntry = new LogEntry { Type = responseObj.Args[0], Message = responseObj.Args[1] };
                this.LogEntries.Insert(0, newLogEntry);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
