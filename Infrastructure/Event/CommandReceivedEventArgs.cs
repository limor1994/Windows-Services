using System;

namespace Infrastructure.Event
{
    /// <summary>
    /// CommandReceivedEventArgs
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CommandReceivedEventArgs : EventArgs
    {

        /// <summary>
        /// Command identifier setter and getter
        /// </summary>
        /// <value>
        /// command identifier
        /// </value>
        public int CommandID { get; set; }

        /// <summary>
        /// CommandReceivedEventArgs Constructor
        /// </summary>
        /// <param name="cmd_id">Command identifier</param>
        /// <param name="arguments">args</param>
        /// <param name="dir_path">directory path</param>
        public CommandReceivedEventArgs(int cmd_id, string[] arguments, string dir_path) 
        {
            //sets the details
            CommandID = cmd_id;
            Args = arguments;
            RequestDirPath = dir_path;
        }

        /// <summary>
        /// Dir path getter and setter
        /// </summary>
        /// <value>
        /// dir path
        /// </value>
        public string RequestDirPath { get; set; }

        /// <summary>
        /// Arguments setter and getter
        /// </summary>
        /// <value>
        /// args
        /// </value>
        public string[] Args { get; set; }

    }
}
