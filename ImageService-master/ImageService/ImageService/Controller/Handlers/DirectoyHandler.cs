using ImageService.Server;
using ImageService.Modal;
using System.IO;
using System;
using System.Linq;
using ImageService.Logging.Modal;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;


namespace ImageService.Controller.Handlers
{
    /// <summary>An implementation of IDirectoryHandler</summary>
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;                  // The Log
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        private readonly string[] m_imageExt = 
            { ".bmp", ".jpg", ".gif", ".png" };             // Image Extensions
        #endregion

        #region Events
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;    // The Event That Notifies that the Directory is being closed
        #endregion

        /// <summary>DirectoryHandler Cntr.</summary>
        /// <param name="log">IImageLogger</param>
        /// <param name="imgController">The Image Processing Controller</param>
        /// <param name="path">The Path of directory</param>
        public DirectoyHandler(ILoggingService log, IImageController imgController, string dirPath)
        {

			this.m_path = dirPath;
            this.m_controller = imgController;
			this.m_logging = log;
            this.m_dirWatcher = new FileSystemWatcher(this.m_path);
        }

		
        /// <summary>The Function Recieves the directory to Handle</summary>
        /// <param name="dirPath">Directory path</param>
        public void StartHandleDirectory(string dirPath)
        {
			//Gets the files in our path.
            string[] filesInDirectory = Directory.GetFiles(m_path);
			m_logging.Log("Begin Start Handle Directory - " + dirPath, MessageTypeEnum.INFO);
			// The foreach adds all files to the output dir
            
			//Add new file system event handler
            this.m_dirWatcher.Created += new FileSystemEventHandler(onDirWatcherCreate);
            this.m_dirWatcher.Changed += new FileSystemEventHandler(onDirWatcherCreate);
            
			//Set the listener to listen
			this.m_logging.Log("Start Handle Directory - " + dirPath, MessageTypeEnum.INFO);
            this.m_dirWatcher.EnableRaisingEvents = true;
            

        }



        /// <summary>Activated on directory change</summary>
        /// <param name="sender"></param>
        /// <param name="e">Arguments of FileSystemEvent.</param>
        private void onDirWatcherCreate(object sender, FileSystemEventArgs e)
        {
            this.m_logging.Log("onDirWatcherCreate - " + e.FullPath, MessageTypeEnum.INFO);
            string extension = Path.GetExtension(e.FullPath);
            // check that the file is an image.
            if (this.m_imageExt.Contains(extension))
            {
                //Gets current arguments of the path
                string[] args = { e.FullPath };
                CommandRecievedEventArgs cmd = new CommandRecievedEventArgs((int)CommandEnum.NewFileCommand, args, "");
                this.OnCommandRecieved(this, cmd);
            }


        }
        /// <summary>The Event that will be activated upon Handler closure</summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The arguments received by the directory close event</param>
        public void OnCloseHandler(object sender, DirectoryCloseEventArgs e)
        {
            try
            {
                // Don't listen to the dir
                this.m_dirWatcher.EnableRaisingEvents = false;
                // Remove from the event OnCommandReceived.         
                ((ImageServer)sender).CommandRecieved -= this.OnCommandRecieved;
                this.m_logging.Log("Path Handler Closed - " + this.m_path, MessageTypeEnum.INFO);
            }
            catch (Exception exc)
            {
                this.m_logging.Log("Failed To Close Path Handler - "+this.m_path+ "-"
                    + exc.ToString(), MessageTypeEnum.FAIL);
            }
        }

		
        /// <summary>The Event that will be activated upon new Command</summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The arguments received by the command event</param>
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool currentResult;
            string message = this.m_controller.ExecuteCommand(e.CommandID, e.Args, out currentResult);
            // According to the current result - INFO if true, FAIL if not
            if (currentResult)
            {
				//Log INFO
                this.m_logging.Log(message, MessageTypeEnum.INFO);
            }
            else
            {
				
				//Log FAIL
                this.m_logging.Log(message, MessageTypeEnum.FAIL);
            }
        }

		
    }
}