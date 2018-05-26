using System.Net.Sockets;
using Infrastructure.Event;
using System.Collections.ObjectModel;


namespace ImageService.Server
{
    /// <summary>
    /// IServerConnection interface
    /// </summary>
    interface IServerConnection
    {

        /// <summary>
        /// Updates the log
        /// </summary>
        /// <param name="object_sender"></param>
        /// <param name="event_args"></param>
        void UpdateLog(object object_sender, CommandReceivedEventArgs event_args);

        /// <summary>
        /// Starts the server
        /// </summary>
        void Start();


        /// <summary>
        /// Gets clients
        /// </summary>
        ObservableCollection<TcpClient> Clients { get; }

    }
}
