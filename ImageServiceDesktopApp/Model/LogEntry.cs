using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp.Model
{
    class LogEntry
    {
        public MessageTypeEnum Type { get; set; }
        public string Message { get; set; }
    }
}
