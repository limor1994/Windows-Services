using Infrastructure.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace ImageServiceWPF.Model
{
    /// <summary>
    /// ILogsModel interface
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    interface ILogsModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Setter and getter of logentries
        /// </summary>
        ObservableCollection<MessageReceivedEventArgs> LogEntries { get; set; }
    }
}
