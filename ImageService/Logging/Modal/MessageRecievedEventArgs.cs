using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging.Modal
{
    /// <summary>
    /// MessageRecievedEventArgs class.
    /// manages the args for log writting
    /// </summary>
    public class MessageRecievedEventArgs : EventArgs
    {
        #region members
        //members
        private MessageTypeEnum m_status;
        private string m_message;
        #endregion
        #region Properties
        public MessageTypeEnum Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }
        #endregion
        /// <summary>
        /// MessageRecievedEventArgs function.
        /// </summary>
        /// <param name="status">type of log writting</param>
        /// <param name="message">message text</param>
        public MessageRecievedEventArgs(MessageTypeEnum status, string message)
        {
            this.m_status = status;
            this.m_message = message;
        }
    }
}
