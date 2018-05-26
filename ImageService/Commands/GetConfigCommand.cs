using ImageService.Model;
using System;

namespace ImageService.Commands
{
    /// <summary>
    /// GetConfigCommand - sends app config
    /// </summary>
    /// <seealso cref="ImageService.Commands.ICommand" />
    class GetConfigCommand : ICommand
    {
        private IImageServiceModel imgModel;



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
                //Tries to build a config
                return imgModel.BuildConfig(out result);
            }
            catch (Exception e)
            {
                //False
                result = false;
                return e.Message;
            }
        }


        /// <summary>
        /// GetConfigCommand Constructor
        /// </summary>
        /// <param name="imgModel">Our model</param>
        public GetConfigCommand(IImageServiceModel imgModel)
        {
            this.imgModel = imgModel;
        }

    }
}
