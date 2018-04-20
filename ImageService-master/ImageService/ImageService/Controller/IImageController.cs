namespace ImageService.Controller
{
    /// <summary>Image Controller Interface.</summary>
    public interface IImageController
    {
        /// <summary>Executes the command</summary>
        /// <param name="commandID">The Command ID</param>
        /// <param name="args">Command Arguments</param>
        /// <param name="result">Whether the comman activated or failed</param>
        /// <return>string</return>
        string ExecuteCommand(int commandID, string[] args, out bool result);
    }
}
