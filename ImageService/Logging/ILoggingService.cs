using ImageService.Logging.Modal;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public delegate void UpdateLogEntry(CommandRecievedEventArgs updateObj);
    /// <summary>
    /// ILoggingService interface.
    /// manages the logging writting
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// MessageRecieved event.
        /// in charge of wriiting message to log
        /// </summary>
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        /// <summary>
        /// message function.
        /// writes the message to log
        /// </summary>
        /// <param name="message"> the message</param>
        /// <param name="type">type of message</param>
        void Log(string message, MessageTypeEnum type);           // Logging the Message
        ObservableCollection<LogEntry> LogMessages { get; set; }   //log entries list
        event UpdateLogEntry UpdateLogEntries;  //Invoked everytime a new event log entry is written to the log
        void InvokeUpdateEvent(string message, MessageTypeEnum type); // Invokes UpdateLogEntries event.
    }
}