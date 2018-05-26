using Infrastructure.Enums;
using ImageService.Logging;
using Infrastructure.Event;
using System;
using System.IO;
using System.Linq;
using Infrastructure.Model;

namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// DirectoryHandler class
    /// </summary>
    /// <seealso cref="ImageService.Controller.Handlers.IDirectoryHandler" />
    public class DirectoryHandler : IDirectoryHandler
    {
        //region members
        private IImageController model_controller;
        private ILoggingService model_logging;
        private FileSystemWatcher model_watcher;
        private string path;
        private string[] extensions = { ".bmp", ".png", ".jpg", ".gif"};
        //end region
        public event EventHandler<DirectoryCloseEventArgs> CloseDirectory;



        /// <summary>
        /// Activates when a new file is created
        /// </summary>
        /// <param name="o_sender">object sender</param>
        /// <param name="event_args">event data</param>
        public void OnCreated(object o_sender, FileSystemEventArgs event_args)
        {
            if (extensions.Contains(Path.GetExtension(event_args.FullPath).ToLower()))
            {
                string[] arguments = { event_args.FullPath };
                CommandReceivedEventArgs commmandArgs = new CommandReceivedEventArgs((int)CommandEnum.NewFileCommand, arguments, path);
                OnCommandReceived(this, commmandArgs);
            }
        }

        /// <summary>
        /// Starts the handler
        /// </summary>
        /// <param name="path_directory">directory path</param>
        public void StartHandleDirectory(string path_directory)
        {
            path = path_directory;
            model_watcher = new FileSystemWatcher(path_directory);
            //A handler is invoked upon new file creation
            model_watcher.Created += new FileSystemEventHandler(OnCreated);
            //start to monitor events
            model_watcher.EnableRaisingEvents = true; 
        }


        /// <summary>
        /// When a handler is closed
        /// </summary>
        /// <param name="o_sender">>object sender</param>
        /// <param name="event_args">event data</param>
        public void OnCloseHandler(object o_sender, DirectoryCloseEventArgs event_args)
        {
            try
            {
                //Unables raising events
                this.model_watcher.EnableRaisingEvents = false;
                Watcher.Created -= new FileSystemEventHandler(OnCreated);
                CloseDirectory?.Invoke(this, new DirectoryCloseEventArgs(path, "Dir " + this.path + " closed"));
            }
            catch (Exception ex)
            {
                //In case an error occured
                this.model_logging.Log("handler closure error: " + this.path + "because of " + ex.Message, MessageTypeEnum.FAIL);
            }
        }


        /// <summary>
        /// Activates when a command is received
        /// </summary>
        /// <param name="o_sender">object sender</param>
        /// <param name="event_args">event data</param>
        public void OnCommandReceived(object o_sender, CommandReceivedEventArgs event_args)
        {
            //Checks the path
            if (event_args.RequestDirPath.Equals(path))
            {
                bool current_result;
                string msg = model_controller.ExecuteCommand(event_args.CommandID, event_args.Args, out current_result);

                //Check if it was executed successfuly
                if (current_result)
                {
                    model_logging.Log("Command of ID: " + event_args.CommandID + " executed successfully", MessageTypeEnum.INFO);
                }
                else
                {
                    //An error
                    model_logging.Log("Error on executing: " + msg, MessageTypeEnum.FAIL);
                }
            }
        }




        /// <summary>
        /// DirectoryHandler Constructor
        /// </summary>
        /// <param name="img_controller">controller</param>
        /// <param name="logging_service">logging service</param>
        public DirectoryHandler(IImageController img_controller, ILoggingService logging_service)
        {
            model_controller = img_controller;
            model_logging = logging_service;
        }

        public FileSystemWatcher Watcher
        {
            get
            {
                return this.model_watcher;
            }
        }


    }
}

