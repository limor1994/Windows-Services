using System;
using ImageService.Modal;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Server;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using ImageService.Controller;

namespace ImageService
{
    /// <summary>ImageService Class</summary>
    public partial class ImageService : ServiceBase
    {

        #region members

        private ImageServer m_imageServer;          // The Image Server
        private IImageServiceModal modal;			//Image Service Modal
        private IImageController controller;		//Image Service Controller
        private ILoggingService logging;			//Service Log
        private int eventId = 1;                    //Changing EventID
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
        #endregion
        

        /// <summary>ImageService Cntr.</summary>
        /// <param name="args">Arguments from command line</param>
        public ImageService(string[] args)
        {
            //Trying to inialize the ImageService members
            try
            {
                InitializeComponent();
                //Getting details from AppConfig
                string log = ConfigurationManager.AppSettings.Get("LogName");
                string sourceName = ConfigurationManager.AppSettings.Get("SourceName");
                //Setting the current log
                eventLog1 = new System.Diagnostics.EventLog();

                //Checks if the source name exists actually
                if (!System.Diagnostics.EventLog.SourceExists(sourceName))
                {
                    //Creates an event sourcename
                    System.Diagnostics.EventLog.CreateEventSource(sourceName, log);
                }
                //Sets for the log the sourcename and log name
                eventLog1.Source = sourceName;
                eventLog1.Log = log;
                //Creating new class members
                this.logging = new LoggingService();
                //Adding the writing message function
                this.logging.MessageRecieved += WriteMessage;
                this.modal = new ImageServiceModal()
                {
                    //Sets the new output directory
                    OutputFolder = ConfigurationManager.AppSettings.Get("OutputDir"),
                    //Gets the config for thumbnail size
                    ThumbnailSize = Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"))

                };
                //Sets the new controller and image server
                this.controller = new ImageController(this.modal);
                this.m_imageServer = new ImageServer(this.controller, this.logging);
            }

            //Catches the exception if any exception was thrown in the process and prints it
            catch (Exception e)
            {
                this.eventLog1.WriteEntry(e.ToString(), EventLogEntryType.Error);
            }
        }
        

        /// <summary>Activates OnTimer</summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">OnTimer function args</param>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {

            //Notifies that currently it monitors the system
            eventLog1.WriteEntry("Listening", EventLogEntryType.Information, eventId++);
        }

        /// <summary>OnContinue func</summary>
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("OnContinue.");
        }


        /// <summary>writes to the log</summary>
        /// <param name="sender"> sender object</param>
        /// <param name="args" >function args</param>
        public void WriteMessage(Object sender, MessageRecievedEventArgs args)
        {
            eventLog1.WriteEntry(args.Message, GetType(args.Status));
        }


        /// <summary>Activates when service activates</summary>
        /// <param name="args">command line args</param>
        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("OnStart");

            //Sets up a new service status
            ServiceStatus serv = new ServiceStatus();
            //Sets as pending
            serv.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            //Decides how much time to wait
            serv.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serv);

            //Activating a timer each minute
            System.Timers.Timer time = new System.Timers.Timer();
            time.Interval = 60000; // 1 minute
            //Check elapsed time
            time.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            time.Start();
            // After the timer is done, start the service 
            serv.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serv);
            //Writing to log
            eventLog1.WriteEntry("Finish OnStart");

        }

        /// <summary>OnStop func</summary>
        protected override void OnStop()
        {
            eventLog1.WriteEntry("onStop.");
            //Closes the server
            this.m_imageServer.OnCloseServer();
            eventLog1.WriteEntry("Finish onStop.");
        }


        /// <summary>Convert to EventLogEntryType from MessageTypeEnum</summary>
        /// <param name="status">log status</param>
        private EventLogEntryType GetType(MessageTypeEnum logStatus)
        {
            //Checks the type of the log status
            switch (logStatus)
            {
                case MessageTypeEnum.WARNING:
                    return EventLogEntryType.Warning;
                case MessageTypeEnum.FAIL:
                    return EventLogEntryType.Error;
                case MessageTypeEnum.INFO:
                //In case doesn't exist, post information
                default:
                    return EventLogEntryType.Information;
            }
        }

    }
}
