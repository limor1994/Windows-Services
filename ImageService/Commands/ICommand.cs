

namespace ImageService.Commands
{
    /// <summary>
    /// Command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute args and returns a result
        /// </summary>
        /// <param name="args">Our arguments</param>
        /// <param name="result">Our result</param>
        /// <returns></returns>
        string Execute(string[] args, out bool result);
    }
}
