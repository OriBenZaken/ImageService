
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    /// <summary>
    /// LoggingService class.
    /// implements ILoggingService interface.
    /// manages the logging writting
    /// </summary>
    public class LoggingService : ILoggingService
    {

        /// <summary>
        /// MessageRecieved event.
        /// in charge of wriiting message to log
        /// </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        public ObservableCollection<LogEntry> LogMessages { get; set;}
        /// <summary>
        /// message function.
        /// writes the message to log
        /// </summary>
        /// <param name="message"> the message</param>
        /// <param name="type">type of message</param>
        ///

        public LoggingService(EventLog eventLog)
        {
            this.LogMessages = new ObservableCollection<LogEntry>();
            GetAllLogEventMessages(eventLog);
        }
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
            this.LogMessages.Add(new LogEntry { Type = Enum.GetName(typeof(MessageTypeEnum), type), Message = message });
        }
        private void GetAllLogEventMessages(EventLog eventLog)
        {
            eventLog.WriteEntry("Enter GetAllLogEventMessages", EventLogEntryType.Warning);
            EventLogEntry[] logs = new EventLogEntry[eventLog.Entries.Count];
            eventLog.Entries.CopyTo(logs, 0);
            foreach (EventLogEntry entry in logs)
            {
                this.LogMessages.Add(new LogEntry { Type = Enum.GetName(typeof(MessageTypeEnum), MessageTypeEnum.INFO), Message = entry.Message });
                string msg = entry.Message;
            }
        }
        // implement
        private MessageTypeEnum FromLogEventTypeToMessageTypeEnum(EventLogEntryType)
        {
            return MessageTypeEnum.INFO;
        }
    }

}
