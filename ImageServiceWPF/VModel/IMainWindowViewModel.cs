using System.Windows.Input;
using System.ComponentModel;


namespace ImageServiceWPF.VModel
{
    /// <summary>
    /// IMainWindowViewModel interface
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    interface IMainWindowViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Getter and setter for disconnect command
        /// </summary>
        ICommand DisconnectCommand { get; set; }

        /// <summary>
        /// Is connected getter
        /// </summary>
        bool VM_IsConnected { get; }

    }
}
