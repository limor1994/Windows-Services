using System;

namespace Infrastructure.Event
{
    /// <summary>
    /// DirectoryCloseEventArgs class
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class DirectoryCloseEventArgs : EventArgs
    {

        /// <summary>
        /// Message getter and setter
        /// </summary>
        /// <value>
        /// message
        /// </value>
        public string Message { get; set; }


        /// <summary>
        /// DirectoryCloseEventArgs Constructor
        /// </summary>
        /// <param name="directory_Path">The dir path.</param>
        /// <param name="msg">The message.</param>
        public DirectoryCloseEventArgs(string directory_Path, string msg)
        {
            DirectoryPath = directory_Path;
            Message = msg;
        }

        /// <summary>
        /// DirectoryPath getter and setter
        /// </summary>
        /// <value>
        /// DirectoryPath
        /// </value>
        public string DirectoryPath { get; set; }


    }
}
