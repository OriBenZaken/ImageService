using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Server;
using ImageService.Controller;
using ImageService.Modal;
using System.Configuration;

namespace ImageService
{
    public partial class ImageService : ServiceBase
    {
        private int eventId = 1;
        private ImageServer m_imageServer;          // The Image Server
        private IImageServiceModal modal;
        private IImageController controller;
        private ILoggingService logging;
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };


        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        public ImageService(string[] args)
        {
            try
            {
                InitializeComponent();

                string eventSourceName = ConfigurationManager.AppSettings.Get("SourceName");
                string logName = ConfigurationManager.AppSettings.Get("LogName");
                //if (args.Count() > 0)
                //{
                //    eventSourceName = args[0];
                //}
                //if (args.Count() > 1)
                //{
                //    logName = args[1];
                //}
                eventLog1 = new System.Diagnostics.EventLog();
                if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
                }
                eventLog1.Source = eventSourceName;
                eventLog1.Log = logName;
                //initialize members
                this.logging = new LoggingService();

                this.logging.MessageRecieved += WriteMessage;
                this.logging.Log("1", MessageTypeEnum.FAIL);
                this.modal = new ImageServiceModal()
                {
                    OutputFolder = ConfigurationManager.AppSettings.Get("OutputDir"),
                    ThumbnailSize = Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"))

                };
                this.controller = new ImageController(this.modal);
                this.m_imageServer = new ImageServer(this.controller, this.logging);
                this.logging.Log("1", MessageTypeEnum.FAIL);

            }
            catch (Exception e)
            {
                this.eventLog1.WriteEntry(e.ToString());
            }
        }



        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");
            eventLog1.WriteEntry("liz1");
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            eventLog1.WriteEntry("leave OnStart");

        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
            ///*****************
            ///

            // Update the service state to Stop Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Update the service state to Stopping.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            //****************
            this.m_imageServer.OnCloseServer();
            eventLog1.WriteEntry("Leave onStop.");


        }
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        public void WriteMessage(Object sender, MessageRecievedEventArgs e)
        {
            eventLog1.WriteEntry(e.Message, GetType(e.Status));
            //eventLog1.WriteEntry(e.Message);
        }
        private EventLogEntryType GetType(MessageTypeEnum status)
        {
            switch (status)
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
