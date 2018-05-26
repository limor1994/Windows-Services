using ImageServiceWPF.Client;
using System.ComponentModel;

namespace ImageServiceWPF.Model
{
    /// <summary>
    /// IMainWindowModel interface
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    interface IMainWindowModel :INotifyPropertyChanged
    {


        /// <summary>
        /// client getter
        /// </summary>
        IClientConnection Client { get; }

        /// <summary>
        /// Setter and Getter for isConnected
        /// </summary>
        bool IsConnected { get; set; }


    }
}
