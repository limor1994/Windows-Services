using ImageService.Commands;
using Infrastructure.Enums;
using ImageService.Model;
using ImageService.Server;
using System;
using System.Collections.Generic;

namespace ImageService.Controller
{
    /// <summary>
    /// ImageController class.
    /// </summary>
    /// <seealso cref="ImageService.Controller.IImageController" />
    public class ImageController : IImageController
    {
        private IImageServiceModel img_model;
        private Dictionary<int, ICommand> commands_list;
        private ImageServer img_server;

        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="commandID">command identifier</param>
        /// <param name="args">args</param>
        /// <param name="exeResult">if the result was successful</param>
        /// <returns></returns>
        public string ExecuteCommand(int commandID, string[] args, out bool exeResult)
        {
            ICommand cmd;
            try
            {
                //Checks if the list has the key
                if (commands_list.ContainsKey(commandID))
                {
                    cmd = commands_list[commandID];
                    return cmd.Execute(args, out exeResult);
                }
                exeResult = false;
                return "Command not found";
            }
            catch (Exception e)
            {
                //In case the execution didn't work
                exeResult = false;
                return e.Message;
            }
        }


        /// <summary>
        /// ImageController Constructor
        /// </summary>
        /// <param name="image_model">the image model</param>
        public ImageController(IImageServiceModel image_model)
        {
            img_model = image_model;
            commands_list = new Dictionary<int, ICommand>()
            {
                {(int)CommandEnum.NewFileCommand, new NewFileCommand(img_model) } ,
                { (int)CommandEnum.GetConfigCommand, new GetConfigCommand(img_model) }
            };
        }

        /// <summary>
        /// Image server setter getter
        /// </summary>
        public ImageServer Server
        {
            get
            {
                return this.img_server;
            }
            set
            {
                this.img_server = value;
                this.commands_list[((int)CommandEnum.CloseCommand)] = new CloseCommand(this.img_model, this.img_server);
                this.commands_list[((int)CommandEnum.LogCommand)] = new LogCommand(this.img_model, this.img_server);
            }

        }

    }
}