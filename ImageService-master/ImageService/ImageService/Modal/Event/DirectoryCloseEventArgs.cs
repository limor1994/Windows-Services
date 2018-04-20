﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    /// <summary>Extends the event arguments</summary>
    public class DirectoryCloseEventArgs : EventArgs
    {
        public string DirectoryPath { get; set; }       // The directory path to close.
		
        public string Message { get; set; }             // The Message That goes to the logger.

        /// <summary>DirectoryCloseEventArgs Cntr.</summary>
        /// <param name="dirPath">Directory Path</param>
        /// <param name="message">Log Message</param>
        public DirectoryCloseEventArgs(string dirPath, string message)
        {
            DirectoryPath = dirPath;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }

    }
}