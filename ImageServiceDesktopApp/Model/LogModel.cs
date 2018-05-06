using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;
using Newtonsoft.Json;

namespace ImageServiceDesktopApp.Model
{
    class LogModel : ILogModel
    {
        private ObservableCollection<LogEntry> logMessages;
        private IImageServiceClient imageServiceClient;
        public LogModel()
        {
            this.logMessages = new ObservableCollection<LogEntry>();
            //this.CreateLogExamples(30);
            this.imageServiceClient = ImageServiceClient.Instance;
            this.intializeLogEntriesList();
        }
        public ObservableCollection<LogEntry> LogMessages
        {
            get
            {
                return this.logMessages;
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
                this.logMessages.Add(new LogEntry { Type = logTypes[type], Message = "Example " + i.ToString() });
            }
        }

        private void intializeLogEntriesList()
        {
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
            this.imageServiceClient.SendCommand(commandRecievedEventArgs);
            try
            {
               // this.logMessages = JsonConvert.DeserializeObject<ObservableCollection<LogEntry>>(result.Args[0]);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }
    }
}
