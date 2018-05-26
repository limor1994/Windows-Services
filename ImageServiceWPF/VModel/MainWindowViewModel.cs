using ImageServiceWPF.Model;
using Prism.Commands;
using System.ComponentModel;
using System.Windows.Input;

namespace ImageServiceWPF.VModel
{
    /// <summary>
    /// MainWindowViewModel
    /// </summary>
    /// <seealso cref="ImageServiceWPF.VModel.IMainWindowViewModel" />
    class MainWindowViewModel : IMainWindowViewModel
    {

        private ICommand cmd;
        public event PropertyChangedEventHandler PropertyChanged;
        private IMainWindowModel main_window_model;


        /// <summary>
        /// Check if can disconnect from argument
        /// </summary>
        /// <param name="argument">arg</param>
        /// <returns>
        /// true
        /// </returns>
        private bool CanDisconnect(object argument)
        {
            return true;
        }


        /// <summary>
        /// MainWindowViewModel constructor
        /// </summary>
        public MainWindowViewModel()
        {

            //Creates a new main window model
            this.main_window_model = new MainWindowModel();
            this.cmd = new DelegateCommand<object>(this.OnDisconnect, this.CanDisconnect);

            //Sets property change delegation
            this.main_window_model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }


        /// <summary>
        /// DisconnectCommand getter setter
        /// </summary>
        /// <value>
        /// DisconnectCommand
        /// </value>
        public ICommand DisconnectCommand { get; set; }

        /// <summary>
        /// In case of property change, it notifies
        /// </summary>
        /// <param name="name">property change name</param>
        public void NotifyPropertyChanged(string name)
        {
            //invokes new
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        /// <summary>
        /// Activates OnDisconnect
        /// </summary>
        /// <param name="the_object">object</param>
        private void OnDisconnect(object the_object)
        {
            this.main_window_model.Client.Disconnect();
        }

        /// <summary>
        /// VM_IsConnected Getter
        /// </summary>
        /// <value>
        /// Gets VM_IsConnected
        /// </value>
        public bool VM_IsConnected
        {
            get
            {
                return main_window_model.IsConnected;
            }
        }


        
    }
}
