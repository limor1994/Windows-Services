using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Configuration;

namespace ImageService.Server
{
	/// <summary>Main ImageServer Class</summary>
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<DirectoryCloseEventArgs> CloseServer;             //Notifies that the directory is closed
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion
		
		
        /// <summary>ImageServer Constructor.</summary>
        /// <param name="controller">IImageController obj</param>
        /// <param name="logging">ILoggingService obj</param>
        public ImageServer(IImageController ctrl, ILoggingService log)
        {
			//Sets the members of the function
            this.m_controller = ctrl;
            this.m_logging = log;
			
			//Creates a list of directories
            string[] dirs = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));

            foreach (string dirPath in dirs)
            {
                try
                {
                    this.createDirHandler(dirPath);
                }
                catch (Exception e)
                {
                    this.m_logging.Log("Creating handler caused error for: " + dirPath + ". Reason:" + e.ToString(), Logging.Modal.MessageTypeEnum.FAIL);
                }
            }
        }
		
		
		/// <summary>OnCloseServer - Server closed details</summary>
        public void OnCloseServer()
        {
				//Tries to close the server
            try
            {
                m_logging.Log("Begining OnCloseServer Function", Logging.Modal.MessageTypeEnum.INFO);
                CloseServer?.Invoke(this, null);
                m_logging.Log("Leaving OnCloseServer Function", Logging.Modal.MessageTypeEnum.INFO);
            }
			//Catches an exception in case it didn't work.
            catch (Exception e)
            {
                this.m_logging.Log("Server Close Error: " + e.ToString(), Logging.Modal.MessageTypeEnum.FAIL);
            }
        }
		
		
		
        /// <summary>Creates dir handler</summary>
        /// <param name="dirPath">The directory handler</param>
        private void createDirHandler(string dirPath)
        {
			//Creates a new directory handler using the logger, controller and directory path.
            IDirectoryHandler directoryDandler = new DirectoyHandler(m_logging, m_controller, dirPath);
			//Adds the handler to the command and the closeserver listener
            CommandRecieved += directoryDandler.OnCommandRecieved;
            this.CloseServer += directoryDandler.OnCloseHandler;
			//Requires the handle to begin to handle the directory
            directoryDandler.StartHandleDirectory(dirPath);
            this.m_logging.Log("New Directory Handler: " + dirPath, Logging.Modal.MessageTypeEnum.INFO);
        }
		
		
        
    }
}
