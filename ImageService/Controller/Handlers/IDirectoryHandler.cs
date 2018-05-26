using Infrastructure.Event;
using System;
using System.IO;

namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// Directory handler interface
    /// </summary>
    public interface IDirectoryHandler
    {

        /// <summary>
        /// Watcher getter
        /// </summary>
        FileSystemWatcher Watcher { get; }


        /// <summary>
        /// Activates when a command is received
        /// </summary>
        /// <param name="o_sender">sender</param>
        /// <param name="event_args">event data</param>
        void OnCommandReceived(object o_sender, CommandReceivedEventArgs event_args);

        /// <summary>
        /// Starts the handle directory
        /// </summary>
        /// <param name="path_directory"></param>
        void StartHandleDirectory(string path_directory);
        event EventHandler<DirectoryCloseEventArgs> CloseDirectory;

        /// <summary>
        /// Activates when a handler is closed
        /// </summary>
        /// <param name="o_sender"></param>
        /// <param name="event_args"></param>
        void OnCloseHandler(object o_sender, DirectoryCloseEventArgs event_args);


        /// <summary>
        /// Activates when a handler is created
        /// </summary>
        /// <param name="o_sender"></param>
        /// <param name="event_args"></param>
        void OnCreated(object o_sender, FileSystemEventArgs event_args);

    }
}
