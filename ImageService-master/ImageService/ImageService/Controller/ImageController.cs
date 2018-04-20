using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>Implementation of Image Controller</summary>
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;             // A dictionary for the commands

        /// <summary>ImageController Cntr.</summary>
        /// <param name="modal">System modal</param>
        public ImageController(IImageServiceModal modal)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>(); //Creating a new dictionary

            // As given - will only contain new file command
            this.commands[((int)CommandEnum.NewFileCommand)] = new NewFileCommand(this.m_modal);
        }

        /// <summary>Executes the command</summary>
        /// <param name="commandID">The Command ID</param>
        /// <param name="args">Command Arguments</param>
        /// <param name="resultSuccesful">Whether the comman activated or failed</param>
        /// <return>string</return>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
			
			//Creating task to execute
            Task<Tuple<string, bool>> executeCommandTask = new Task<Tuple<string, bool>>(() => {
               
			   bool flag;
               string cmd = this.commands[commandID].Execute(args, out flag);
               return Tuple.Create(cmd, flag);
            });
			
			//Start the task
            executeCommandTask.Start();
			executeCommandTask.Wait();
            Tuple<string, bool> r = executeCommandTask.Result;
			
			//Get the result
            resultSuccesful = r.Item2;
            return r.Item1;
        }
    }
}
