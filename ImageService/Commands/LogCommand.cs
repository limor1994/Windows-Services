using ImageService.Model;
using ImageService.Server;
using System;

namespace ImageService.Commands
{
    /// <summary>
    /// LogCommand - logs the actions
    /// </summary>
    /// <seealso cref="ImageService.Commands.ICommand" />
    class LogCommand : ICommand
    {
        private IImageServiceModel imgModel;
        private ImageServer imgServer;


        /// <summary>
        /// Execute args and returns a result
        /// </summary>
        /// <param name="args">Our arguments</param>
        /// <param name="result">Our result</param>
        /// <returns></returns>
        public string Execute(string[] args, out bool result)
        {
            try
            {
                //Updates the enteries
                return imgModel.UpdateEntries(this.imgServer, out result);
            }
            catch (Exception e)
            {
                result = false;
                return e.Message;
            }
        }

        /// <summary>
        /// LogCommand Constructor
        /// </summary>
        /// <param name="imgModel">Our model</param>
        /// <param name="imgServer">Our server</param>
        public LogCommand(IImageServiceModel imgModel, ImageServer imgServer)
        {
            this.imgModel = imgModel;
            this.imgServer = imgServer;
        }

    }
}
