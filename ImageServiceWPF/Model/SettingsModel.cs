using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Infrastructure;
using Infrastructure.Enums;
using ImageServiceWPF.Client;
using Newtonsoft.Json.Linq;
using Infrastructure.Event;
using System.Windows;

namespace ImageServiceWPF.Model
{
    /// <summary>
    /// settings model class
    /// </summary>
    /// <seealso cref="ImageServiceWPF.Model.ISettingsModel" />
    class SettingsModel : ISettingsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string name_source;
        private string dir_output;
        private string name_log;
        private int size_thumb;
        private ObservableCollection<string> handlers;
        private string handler;

        /// <summary>
        /// SettingsModel Constructor
        /// </summary>
        public SettingsModel()
        {

            //Set handlers and initialize request
            handlers = new ObservableCollection<string>();
            this.Connection.DataReceived += OnDataReceived;
            
            CommandReceivedEventArgs r = new CommandReceivedEventArgs((int)CommandEnum.GetConfigCommand, null, null);
            this.Connection.Initialize(r);
        }


        /// <summary>
        /// Setter and Getter for OutputDirectory
        /// </summary>
        /// <value>
        /// OutputDirectory
        /// </value>
        public string OutputDirectory
        {
            set
            {
                //notifies as well
                this.dir_output = value;
                this.NotifyPropertyChanged("OutputDirectory");
            }
            get
            {
                return this.dir_output;
            }
        }


        /// <summary>
        /// When data is received
        /// </summary>
        /// <param name="object_sender">object sender</param>
        /// <param name="command_msg">message</param>
        public void OnDataReceived(object object_sender, CommandMessage command_msg)
        {

            //Checks if the command ID is equal
            if (command_msg.CommandID.Equals((int)CommandEnum.GetConfigCommand))
            {
                try
                {
                    //Invokes a new action
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        //Sets outputdir, sourcename, logname, thumbnailsize and more
                        this.OutputDirectory = (string)command_msg.CommandArgs["OutputDirectory"];
                        this.SourceName = (string)command_msg.CommandArgs["SourceName"];
                        this.LogName = (string)command_msg.CommandArgs["LogName"];
                        this.ThumbnailSize = (int)command_msg.CommandArgs["ThumbnailSize"];
                        JArray jarray = (JArray)command_msg.CommandArgs["Handlers"];
                        //Changes to string array
                        string[] string_array = jarray.Select(c => (string)c).ToArray();
                        //adds items to handlers
                        foreach (var item in string_array)
                        {
                            this.Handlers.Add(item);
                        }

                    }));

                }
                catch (Exception e)
                {
                    //In case of an error
                    Console.WriteLine(e.Message);
                }
            }
            
            //Checks the command ID
            if (command_msg.CommandID.Equals((int)CommandEnum.CloseCommand))
            {
                try
                {
                    //Invokes an action
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {

                        //Sets handler remove
                        this.Handlers.Remove((string)command_msg.CommandArgs["HandlerRemoved"]);
                    }));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// NotifyPropertyChanged
        /// </summary>
        /// <param name="name">name</param>
        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }



        /// <summary>
        /// Setter and Getter for LogName
        /// </summary>
        /// <value>
        /// LogName
        /// </value>
        public string LogName
        {
            set
            {
                this.name_log = value;
                this.NotifyPropertyChanged("LogName");
            }
            get
            {
                return this.name_log;
            }
        }



        /// <summary>
        /// Setter and Getter for SourceName
        /// </summary>
        /// <value>
        /// SourceName
        /// </value>
        public string SourceName
        {
            set
            {
                this.name_source = value;
                this.NotifyPropertyChanged("SourceName");
            }
            get
            {
                return this.name_source;
            }
        }


        /// <summary>
        /// Setter and Getter for SelectedHandler
        /// </summary>
        /// <value>
        /// SelectedHandler
        /// </value>
        public string SelectedHandler
        {
            get
            {
                return this.handler;
            }
            set
            {
                handler = value;
                this.NotifyPropertyChanged("SelectedHandler");
            }
        }

        /// <summary>
        /// Setter and Getter for ThumbnailSize
        /// </summary>
        /// <value>
        /// ThumbnailSize
        /// </value>
        public int ThumbnailSize
        {
            set
            {
                this.size_thumb = value;
                this.NotifyPropertyChanged("ThumbnailSize");
            }
            get
            {
                return this.size_thumb;
            }
        }

        /// <summary>
        /// Getter for IClientConnection
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public IClientConnection Connection
        {
            get
            {
                return ClientConnection.Instance;
            }
        }

        /// <summary>
        /// Setter and Getter for Handlers
        /// </summary>
        /// <value>
        /// Handlers
        /// </value>
        public ObservableCollection<string> Handlers
        {
            get
            {
                return this.handlers;
            }
            set
            {
                //Sets and notifies
                this.handlers = value;
                this.NotifyPropertyChanged("Handlers");
            }
        }


    }
}

