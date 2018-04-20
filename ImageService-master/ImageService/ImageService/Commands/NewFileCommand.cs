using ImageService.Modal;
using System;
using System.IO;

namespace ImageService.Commands
{
    /// <summary>Adds a new file to the OutputDir.</summary>
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal m_modal;


        /// <summary>NewFileCommand Cntr.</summary>
        /// <param name="modal">IImageModal</param>
        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;            
        }

        /// <summary>Executes the command</summary>
        /// <param name="args">string array - args </param>
        /// <param name="result"> out bool - result</param>
        /// <returns>returns back a string that describes what the command does</returns>
        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and if not will return the error message
            try
            {
				//First check if there are arguments
				//Adds the gile to the outputDir using the modal.
                string path = args[0];
                if (args.Length==0)
                {
                    throw new Exception("NO ARGUMENTS");
                }
				//Else ifhecks if the file exists
                else if (File.Exists(path))
                {
                    
                    return m_modal.AddFile(path, out result);
                }
				
				//Since nothing has returned, return an empty string.
				result = true;
				string empty = string.Empty;
                return empty;

            }
			//Catches the exception
            catch (Exception e)
            {
                result = false;
                return e.ToString();
            }
        }
    }
}
