using System;

namespace ImageService.Modal
{
    /// <summary>Extends the EventArgs</summary>
    public class CommandRecievedEventArgs : EventArgs
    {
        public int CommandID { get; set; }      // The Command ID
        public string[] Args { get; set; }		//Arguments
        public string RequestDirPath { get; set; }  // The Request Directory

        /// <summary>CommandRecievedEventArgs Cntr.</summary>
        /// <param name="id">The Command ID</param>
        /// <param name="args">Command Arguments</param>
        /// <param name="path">Command File Path</param>
        public CommandRecievedEventArgs(int id, string[] args, string path)
        {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }
    }
}