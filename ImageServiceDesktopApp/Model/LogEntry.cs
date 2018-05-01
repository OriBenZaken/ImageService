﻿using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceDesktopApp.Model
{
    class LogEntry : INotifyPropertyChanged
    {
        private MessageTypeEnum type;
        public string Type
        {
            get { return Enum.GetName(typeof(MessageTypeEnum), type); }
            set { this.type = (MessageTypeEnum) Enum.Parse(typeof(MessageTypeEnum), value); }
        }
        public string Message { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
