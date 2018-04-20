using ImageService.Modal;
using System;

namespace ImageService.Controller.Handlers
{
    /// <summary>A listener interface that notifies the functions in case a file is created/moved in the folder</summary>
    public interface IDirectoryHandler
    { 
	
		// The Event That Notifies that the Directory is being closed
        event EventHandler<DirectoryCloseEventArgs> DirectoryClose;               
        /// <summary>The Function Recieves the directory to Handle</summary>
        /// <param name="dirPath">Directory path</param>
        void StartHandleDirectory(string dirPath);

        /// <summary>The Event that will be activated upon new Command</summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The arguments received by the command event</param>
        void OnCommandRecieved(object sender, CommandRecievedEventArgs e);		

        /// <summary>The Event that will be activated upon Handler closure</summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The arguments received by the directory close event</param>
        void OnCloseHandler(object sender, DirectoryCloseEventArgs e);
    }
}
