using ImageService.Controller;
using System.IO;
using System.Threading;
using Infrastructure.Model;
using Infrastructure.Event;
using System;
using System.Collections.Generic;
using ImageService.Controller.Handlers;
using ImageService.Logging;
using ImageService.Model;


namespace ImageService.Server
{
    /// <summary>
    /// ImageServer
    /// </summary>
    public class ImageServer
    {
        //regionMembers
        private IImageController model_controller;
        private ILoggingService model_logging;
        private Dictionary<string, IDirectoryHandler> handlers_list;

        //endregion

        public event EventHandler<CommandReceivedEventArgs> CommandReceived; 
        public event EventHandler<DirectoryCloseEventArgs> CloseServer;
        //endregion



        /// <summary>
        /// receive logging
        /// </summary>
        /// <value>
        /// logging
        /// </value>
        public ILoggingService Logging
        {
            get { return this.model_logging; }
        }


        /// <summary>
        /// ImageServer Constructor
        /// </summary>
        /// <param name="logging">logging</param>
        /// <param name="dir_output">directory</param>
        /// <param name="size_of_thumb">thumbnail size</param>
        /// <param name="handler">handler</param>
        public ImageServer(ILoggingService logging, IImageController controller, string dir_output, int size_of_thumb, string handler)
        {
            //creates a new image service
            IImageServiceModel img_services = new ImageServiceModel(dir_output, size_of_thumb);
            model_controller = new ImageController(img_services);
            handlers_list = new Dictionary<string, IDirectoryHandler>();
            model_logging = logging;
            //creates array of dirs
            string[] dirs = handler.Split(';');
            foreach (string path in dirs)
            {
                try
                {
                    CreateHandler(path);
                }
                catch (Exception e)
                {
                    this.model_logging.Log("handler creation error for: " + path + "because of " + e.Message, MessageTypeEnum.FAIL);
                }

            }
        }

        /// <summary>
        /// receive handler
        /// </summary>
        /// <value>
        /// handler
        /// </value>
        public Dictionary<string, IDirectoryHandler> Handlers
        {
            get { return this.handlers_list; }
        }

        /// <summary>
        /// In case the server is closed
        /// </summary>
        public void OnCloseServer()
        {
            try
            {
                CloseServer?.Invoke(this, null);
                model_logging.Log("Notified handler", MessageTypeEnum.INFO);
            }
            catch (Exception e)
            {
                this.model_logging.Log("Handler notification error" + e.Message, MessageTypeEnum.FAIL);
            }
        }


        /// <summary>
        /// send command
        /// </summary>
        /// <param name="event_argument">event data</param>
        public void SendCommand(CommandReceivedEventArgs event_argument)
        {
            CommandReceived?.Invoke(this, event_argument);
        }

        /// <summary>
        /// Closes a specific handler
        /// </summary>
        /// <param name="specific_handler">handler to del</param>
        public void CloseSpecifiedHandler(string specific_handler)
        {
            //Check if we have that handler
            if (handlers_list.ContainsKey(specific_handler))
            {
                IDirectoryHandler dir_hand = handlers_list[specific_handler];
                this.CloseServer -= dir_hand.OnCloseHandler;
                //on close
                dir_hand.OnCloseHandler(this, null);
            }
        }


        /// <summary>
        /// Removes a handler
        /// </summary>
        /// <param name="object_sender">object sender</param>
        /// <param name="event_arguments">event data</param>
        public void RemoveHandler(object object_sender, DirectoryCloseEventArgs event_arguments)
        {
            DirectoryHandler dir_handler = (DirectoryHandler) object_sender;
            CommandReceived -= dir_handler.OnCommandReceived;
            CloseServer -= dir_handler.OnCloseHandler;
            dir_handler.CloseDirectory -= RemoveHandler;
            model_logging.Log("The " + event_arguments.Message + " dir was closed", MessageTypeEnum.INFO);
        }


        /// <summary>
        /// create handlers
        /// </summary>
        /// <param name="dir">directory</param>
        public void CreateHandler(string dir)
        {
            Thread.Sleep(1000);
            //checks if path exists
            if (!Directory.Exists(dir))
            {
                model_logging.Log("File doesn't exist", MessageTypeEnum.FAIL);
                throw new FileNotFoundException();
            }

            //new directory handler
            DirectoryHandler directory_handler = new DirectoryHandler(model_controller, model_logging);
            handlers_list[dir] = directory_handler;
            CommandReceived += directory_handler.OnCommandReceived;
            CloseServer += directory_handler.OnCloseHandler;
            //adds the remove handler, and starts to handle the directory
            directory_handler.CloseDirectory += RemoveHandler;
            directory_handler.StartHandleDirectory(dir);
        }



    }
}