using ImageService.Server;

namespace ImageService.Model
{
    /// <summary>
    /// the image service model interface.
    /// </summary>
    public interface IImageServiceModel
    {



        /// <summary>
        /// Builds the config
        /// </summary>
        /// <param name="operation_result"></param>
        /// <returns></returns>
        string BuildConfig(out bool operation_result);


        /// <summary>
        /// Adds a new file
        /// </summary>
        /// <param name="dir_path">directory path</param>
        /// <param name="operation_result">if the operation was successful</param>
        /// <returns></returns>
        string AddFile(string dir_path, out bool operation_result);

        /// <summary>
        /// Updates enteries
        /// </summary>
        /// <param name="img_server"></param>
        /// <param name="operation_result"></param>
        /// <returns></returns>
        string UpdateEntries(ImageServer img_server, out bool operation_result);


        /// <summary>
        /// Removed message
        /// </summary>
        /// <param name="handlerRemoved"></param>
        /// <param name="operation_result"></param>
        /// <returns></returns>
        string BuildHandlerRemovedMessage(string handlerRemoved, out bool operation_result);

    }
}
