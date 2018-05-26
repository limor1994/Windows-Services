using ImageService.Model;
using System;

namespace ImageService.Commands
{
    /// <summary>
    /// NewFileCommand - Adds a new file
    /// </summary>
    /// <seealso cref="ImageService.Commands.ICommand" />
    public class NewFileCommand : ICommand
    {
        private IImageServiceModel imgModel;


        /// <summary>
        /// Execute args and returns a result
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns></returns>
        public string Execute(string[] args, out bool result)
        {
            //Gets the path to the new file
            string newFilePath = args[0];
            try
            {
                //Adds a new file
                return this.imgModel.AddFile(newFilePath, out result);
            } catch (Exception e)
            {
                result = false;
                return e.Message;
            }
        }


        /// <summary>
        /// NewFileCommand Constructor
        /// </summary>
        /// <param name="imgModel">Our model</param>
        public NewFileCommand(IImageServiceModel imgModel)
        {
            this.imgModel = imgModel;
        }

    }
}
