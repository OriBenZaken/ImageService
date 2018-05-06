
using ImageService.Infrastructure.Enums;
using ImageService.Logging.Modal;
using ImageService.Modal;
using Newtonsoft.Json;
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
        public event UpdateLogEntry UpdateLogEntries;

        private ObservableCollection<LogEntry> logMessages;
        public ObservableCollection<LogEntry> LogMessages {
            get { return this.logMessages; }
            set => throw new NotImplementedException();
        }
        /// <summary>
        /// message function.
        /// writes the message to log
        /// </summary>
        /// <param name="message"> the message</param>
        /// <param name="type">type of message</param>
        ///

        public LoggingService(EventLog eventLog)
        {
            this.logMessages = new ObservableCollection<LogEntry>();
            GetAllLogEventMessages(eventLog);
        }
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
            LogEntry newLogEnrty = new LogEntry { Type = Enum.GetName(typeof(MessageTypeEnum), type), Message = message };
            this.LogMessages.Insert(0, newLogEnrty);

            string[] args = new string[2];
            args[0] = newLogEnrty.Type;
            args[1] = newLogEnrty.Message;
            CommandRecievedEventArgs updateObj = new CommandRecievedEventArgs((int)CommandEnum.AddLogEntry, args, null);
            try
            {
                this.UpdateLogEntries?.Invoke(updateObj);
            } catch (Exception ex)
            {

            }
        }
        private void GetAllLogEventMessages(EventLog eventLog)
        {
            eventLog.WriteEntry("Enter GetAllLogEventMessages", EventLogEntryType.Warning);
            EventLogEntry[] logs = new EventLogEntry[eventLog.Entries.Count];
            eventLog.Entries.CopyTo(logs, 0);
            foreach (EventLogEntry entry in logs)
            {
                this.LogMessages.Insert(0, new LogEntry { Type = Enum.GetName(typeof(MessageTypeEnum), LoggingService.FromLogEventTypeToMessageTypeEnum(entry.EntryType)),
                    Message = entry.Message });
            }
        }

        public static MessageTypeEnum FromLogEventTypeToMessageTypeEnum(EventLogEntryType type)
        {
            switch (type)
            {
                case EventLogEntryType.Information:
                    return MessageTypeEnum.INFO;
                case EventLogEntryType.Warning:
                    return MessageTypeEnum.WARNING;
                case EventLogEntryType.Error:
                default:
                    return MessageTypeEnum.FAIL;
            }
        }
        
        public static EventLogEntryType FromMessageTypeEnumToEventLogEntryType(MessageTypeEnum type)
        {
            switch (type)
            {
                case MessageTypeEnum.FAIL:
                    return EventLogEntryType.Error;
                case MessageTypeEnum.WARNING:
                    return EventLogEntryType.Warning;
                case MessageTypeEnum.INFO:
                default:
                    return EventLogEntryType.Information;
            }
        }
    }

}
