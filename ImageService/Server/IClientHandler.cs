using ImageService.Controller;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using Infrastructure.Event;
using System.Threading;
using System;

namespace ImageService.Server
{
    /// <summary>
    /// IClientHandler interface
    /// </summary>
    public interface IClientHandler
    {

       

        /// <summary>
        /// Sends response to client
        /// </summary>
        /// <param name="img_controller"></param>
        /// <param name="command"></param>
        /// <param name="binary_writer"></param>
        /// <returns></returns>
        Boolean SendToClient(IImageController img_controller, CommandReceivedEventArgs command, BinaryWriter binary_writer);

        /// <summary>
        /// Closes connection
        /// </summary>
        void Close();

        /// <summary>
        /// mutex getter setter
        /// </summary>
        Mutex M_mutex { get; set; }

        /// <summary>
        /// Handles a given client
        /// </summary>
        /// <param name="tcp_client"></param>
        /// <param name="img_controller"></param>
        /// <param name="clients"></param>
        void HandleClient(TcpClient tcp_client, IImageController img_controller, ObservableCollection<TcpClient> clients);


    }
}
