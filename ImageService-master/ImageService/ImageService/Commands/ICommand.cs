namespace ImageService.Commands
{
    /// <summary>An Interfaace of a Command.</summary>
    public interface ICommand
    {
        /// <summary>Executes the command</summary>
        /// <param name="args">string array - args </param>
        /// <param name="result"> out bool - result</param>
        /// <returns>returns back a string that describes what the command does</returns>
        string Execute(string[] args, out bool result); 
    }
}
