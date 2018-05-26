using ImageService.Server;

namespace ImageService.Controller
{
    /// <summary>
    /// Image Controller class
    /// </summary>
    public interface IImageController
    {
        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="commandID">The identifier</param>
        /// <param name="args">args</param>
        /// <param name="exeResult">result</param>
        /// <returns></returns>
        string ExecuteCommand(int commandID, string[] args, out bool exeResult);


        /// <summary>
        /// Image server getter setter
        /// </summary>
        ImageServer Server { get; set; }
    }
}
