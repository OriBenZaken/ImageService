using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LogEntry 
    {
        private MessageTypeEnum type;
        public string Type
        {
            get { return Enum.GetName(typeof(MessageTypeEnum), type); }
            set { this.type = (MessageTypeEnum) Enum.Parse(typeof(MessageTypeEnum), value); }
        }
        public string Message { get; set; }

    }
}
