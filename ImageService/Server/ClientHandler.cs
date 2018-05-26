using System;
using System.Threading.Tasks;
using System.Threading;
using ImageService.Logging;
using Infrastructure.Model;
using System.Collections.ObjectModel;
using ImageService.Controller;
using Infrastructure.Enums;
using Newtonsoft.Json;
using Infrastructure.Event;
using System.IO;
using System.Net.Sockets;

namespace ImageService.Server
{
    /// <summary>
    /// ClientHandler - handles our client
    /// </summary>
    /// <seealso cref="ImageService.Server.IClientHandler" />
    public class ClientHandler : IClientHandler
    {
        private CancellationTokenSource tSource;
        private ILoggingService model_logging_service;



        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="tcp_client">The client.</param>
        /// <param name="img_controller">The controller.</param>
        /// <param name="clients">The clients.</param>
        public void HandleClient(TcpClient tcp_client, IImageController img_controller, ObservableCollection<TcpClient> clients)
        {

            //creates a new task
            new Task(() =>
            {
                try
                {
                    while (true)
                    {

                        //creates a stream with reader and writer
                        NetworkStream client_stream = tcp_client.GetStream();
                        BinaryReader b_reader = new BinaryReader(client_stream);
                        BinaryWriter b_writer = new BinaryWriter(client_stream);
                        string string_input = b_reader.ReadString();
                        if (string_input != null)
                        {
                            //Gets the command received
                            CommandReceivedEventArgs cmd = JsonConvert.DeserializeObject<CommandReceivedEventArgs>(string_input);
                            if (cmd.CommandID.Equals((int)CommandEnum.CloseGUI))
                            {
                                //removes the client
                                clients.Remove(tcp_client);
                                tcp_client.Close();
                                break;
                            }
                            else
                            {
                                //sends the arguments to the client
                                if (!this.SendToClient(img_controller, cmd, b_writer))
                                {
                                    clients.Remove(tcp_client);
                                    tcp_client.Close();
                                    break;
                                }
                            }


                        }
                    }
                }
                catch (Exception e)
                {
                    //In case of an issue, cancels the token
                    this.tSource.Cancel();
                    model_logging_service.Log("Server Failure: " + e.Message, MessageTypeEnum.FAIL);
                }
            }, this.tSource.Token).Start();
        }


        /// <summary>
        /// ClientHandler constructor
        /// </summary>
        /// <param name="m_logger">logging service</param>
        public ClientHandler(ILoggingService m_logger)
        {
            //creates a new cancellation token
            this.tSource = new CancellationTokenSource();
            this.model_logging_service = m_logger;
        }

        //A mutex to be used
        public Mutex M_mutex { get; set; }


        /// <summary>
        /// Executes command, uses mutex and sends to client
        /// </summary>
        /// <param name="img_controller">image controller</param>
        /// <param name="command">event data</param>
        /// <param name="binary_writer">binary writer</param>
        /// <returns></returns>
        public Boolean SendToClient(IImageController img_controller, CommandReceivedEventArgs command, BinaryWriter binary_writer)
        {
            try
            {
                bool send_result;
                string msg = img_controller.ExecuteCommand(command.CommandID, command.Args, out send_result);
                M_mutex.WaitOne();
                binary_writer.Write(msg);
                M_mutex.ReleaseMutex();
                return true;
            }
            catch (Exception e)
            {
                model_logging_service.Log("Client Sending Failure: " + e.Message, MessageTypeEnum.FAIL);
                return false;
            }
        }


        /// <summary>
        /// Closes the token source
        /// </summary>
        public void Close()
        {
            this.tSource.Cancel();
        }




    }

}
