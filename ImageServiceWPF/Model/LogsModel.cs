using System;
using Infrastructure.Event;
using ImageServiceWPF.Client;
using Newtonsoft.Json;
using System.Windows;
using Infrastructure;
using Infrastructure.Model;
using Infrastructure.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImageServiceWPF.Model
{
    /// <summary>
    /// ILogsModel interface
    /// </summary>
    /// <seealso cref="ImageServiceWPF.Model.ILogsModel" />
    class LogsModel : ILogsModel
    {
        private ObservableCollection<MessageReceivedEventArgs> logs_enteries;
        public event PropertyChangedEventHandler PropertyChanged;



        /// <summary>
        /// When a data is received
        /// </summary>
        /// <param name="object_sender">sender</param>
        /// <param name="command_msg">message</param>
        public void OnDataReceived(object object_sender, CommandMessage command_msg)
        {
            //Checks if the command is the same
            if (command_msg.CommandID.Equals((int)CommandEnum.LogCommand))
            {
                try
                {
                    //dispatches a new action
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        //desiarilize list 
                        string list = (string)command_msg.CommandArgs["LogEntries"];
                        ObservableCollection<MessageReceivedEventArgs> collection = JsonConvert.DeserializeObject<ObservableCollection<MessageReceivedEventArgs>>(list);
                        this.LogEntries = collection;
                    }));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        /// <summary>
        /// LogsModel Constructor
        /// </summary>
        public LogsModel()
        {
            //Adds data received
            this.Connection.DataReceived += OnDataReceived;
            CommandReceivedEventArgs cmd = new CommandReceivedEventArgs((int)CommandEnum.LogCommand, null, null);
            //To add - this.Connection.Initialize(request);
            this.Connection.Read();
        }





        /// <summary>
        /// Log entries setter and getter
        /// </summary>
        /// <value>
        /// log entries
        /// </value>
        public ObservableCollection<MessageReceivedEventArgs> LogEntries
        {
            get
            {
                return this.logs_enteries;
            }
            set
            {
                //notifies as well
                this.logs_enteries = value;
                NotifyPropertyChanged("LogEntries");
            }
        }



        /// <summary>
        /// returns the client connection instance
        /// </summary>
        /// <value>
        /// connection
        /// </value>
        public IClientConnection Connection
        {
            get
            {
                return ClientConnection.Instance;
            }
        }


        /// <summary>
        /// Notify that a property has changed
        /// </summary>
        /// <param name="name">property name</param>
        private void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



    }
}
