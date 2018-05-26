using ImageServiceWPF.Client;
using System.ComponentModel;

namespace ImageServiceWPF.Model
{
    /// <summary>
    /// main window model class
    /// </summary>
    /// <seealso cref="ImageServiceWPF.Model.IMainWindowModel" />
    public class MainWindowModel : IMainWindowModel
    {

        private IClientConnection client_connection;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool is_connected;


        /// <summary>
        /// Getter for client_connection
        /// </summary>
        /// <value>
        /// client_connection
        /// </value>
        public IClientConnection Client
        {
            get
            {
                return this.client_connection;
            }
        }


        /// <summary>
        /// MainWindowModel Constructor
        /// </summary>
        public MainWindowModel()
        {
            //creates an instance of client connection
            //Checks if connected
            client_connection = ClientConnection.Instance;
            IsConnected = client_connection.IsConnected;
        }



        /// <summary>
        /// In case of property change, notifies.
        /// </summary>
        /// <param name="name">name</param>
        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        /// <summary>
        /// Getter and Setter for IsConnected
        /// </summary>
        /// <value>
        /// IsConnected
        /// </value>
        public bool IsConnected
        {
            get { return is_connected; }
            set
            {
                //Seter the IsConnected
                is_connected = value;
                this.NotifyPropertyChanged("IsConnected");
            }
        }

    }
}
