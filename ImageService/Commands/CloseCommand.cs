using ImageService.Model;
using ImageService.Server;
using System;

namespace ImageService.Commands
{
    /// <summary>
    /// CloseCommand - Closes a handler
    /// </summary>
    /// <seealso cref="ImageService.Commands.ICommand" />
    class CloseCommand : ICommand
    {
        private ImageServer imgServer;
        private IImageServiceModel imgModel;


        /// <summary>
        /// Execute args and returns a result
        /// </summary>
        /// <param name="args">Our argument</param>
        /// <param name="result">Our result</param>
        /// <returns></returns>
        /// <exception cref="Exception">invalid arguments</exception>
        public string Execute(string[] args, out bool result)
        {
            try
            {
                //Checks for a problem in the arguments
                if (args == null || args.Length == 0)
                {
                    throw new Exception("invalid arguments");
                }

                //Closes the handler
                imgServer.CloseSpecifiedHandler(args[0]);
               
                result = true;
                return imgModel.BuildHandlerRemovedMessage(args[0], out result); 
            }
            catch (Exception e)
            {
                //Sets the result to false
                result = false;
                return e.ToString();
            }
        }


        /// <summary>
        /// CloseCommand Constructor
        /// </summary>
        /// <param name="imgModel">Our model</param>
        /// <param name="imgServer">Our server</param>
        public CloseCommand(IImageServiceModel imgModel, ImageServer imgServer)
        {
            this.imgModel = imgModel;
            this.imgServer = imgServer;
        }

    }
}
