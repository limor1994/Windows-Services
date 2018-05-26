using ImageService.Controller;
using Infrastructure.Event;
using System.Threading;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ImageService.Logging;
using Infrastructure.Model;
using Infrastructure.Enums;
using System;
using System.Net.Sockets;

namespace ImageService.Server
{
    /// <summary>
    /// server connection class
    /// </summary>
    /// <seealso cref="ImageService.Server.IServerConnection" />
    class ServerConnection : IServerConnection
    {
        private static Mutex model_mutex = new Mutex();
        private int server_port;
        private TcpListener tcp_listener;
        private ObservableCollection<TcpClient> clients_list;
        private bool server_stopped;
        private IImageController img_controller;
        private IClientHandler client_handler;
        private ILoggingService logging_service;


        /// <summary>
        /// server start
        /// </summary>
        public void Start()
        {

            //end point set to 127.0.0.1 and starts a listener
            IPEndPoint end_point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), server_port);
            tcp_listener = new TcpListener(end_point);
            tcp_listener.Start();


            //Starts a new task
            Task task = new Task(() =>
            {
                //as long as the server is running
                while (!server_stopped)
                {
                    try
                    {
                        //Sets the tcp to accept client
                        TcpClient current_client = tcp_listener.AcceptTcpClient();
                        Clients.Add(current_client);
                        client_handler.HandleClient(current_client, img_controller, Clients);
                        //Updates that the client is connected
                        logging_service.Log("The client connected", MessageTypeEnum.INFO);
                    }
                    catch (SocketException e)
                    {
                        logging_service.Log(e.Message, MessageTypeEnum.FAIL);
                    }
                }
                //Notifies that the server stopped
                logging_service.Log("Server has stopped", MessageTypeEnum.INFO);
            });
            task.Start();
        }


        /// <summary>
        /// ServerConnection Constructor
        /// </summary>
        /// <param name="img_ctrl">The m controller.</param>
        /// <param name="log_service">The m logging.</param>
        /// <param name="port">The port.</param>
        public ServerConnection(IImageController img_ctrl, ILoggingService log_service, int port)
        {
            //sets image controller and log service
            this.img_controller = img_ctrl;
            this.logging_service = log_service;
            log_service.NewLogEntry += UpdateLog;
            this.server_port = port;

            //Creates a new client handler
            this.client_handler = new ClientHandler(log_service);
            this.client_handler.M_mutex = model_mutex;
            
            //sets the server as running
            this.server_stopped = false;
            this.clients_list = new ObservableCollection<TcpClient>();
        }

        /// <summary>
        /// receive clients
        /// </summary>
        /// <value>
        /// clients
        /// </value>
        public ObservableCollection<TcpClient> Clients
        {
            get
            {
                return this.clients_list;
            }
        }

        

        /// <summary>
        /// Closes communication
        /// </summary>
        public void CloseCommunication()
        {
            //Notify server closure, iterates over each client
            try
            {
                foreach (TcpClient client in Clients)
                {
                    client.Close();
                }
                //sets the server to stop
                this.server_stopped = true;
                this.tcp_listener.Stop();
            }
            catch (Exception e)
            {
                logging_service.Log("Server stopping error" + e.Message, MessageTypeEnum.FAIL);
            }
        }


        /// <summary>
        /// updates the log with the message
        /// </summary>
        /// <param name="object_sender">object sender</param>
        /// <param name="event_args">event data</param>
        public void UpdateLog(object object_sender, CommandReceivedEventArgs event_args)
        {

            try
            {

                //runs over each client and creates tasks
                bool message_result;
                foreach (TcpClient client in Clients)
                {

                    //creates a new task
                    new Task(() =>
                    {
                        try
                        {
                            //check if the command id is as in the enum
                            if (event_args.CommandID.Equals((int)CommandEnum.LogCommand))
                            {
                                NetworkStream network_stream = client.GetStream();
                                BinaryWriter binary_writer = new BinaryWriter(network_stream);
                                string msg = img_controller.ExecuteCommand(event_args.CommandID, event_args.Args, out message_result);
                                //uses mutex to close the data
                                model_mutex.WaitOne();
                                //writes the message
                                binary_writer.Write(msg);
                                model_mutex.ReleaseMutex();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }).Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



       
    }
}
