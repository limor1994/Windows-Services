using System.ComponentModel;
using System.Collections.ObjectModel;
using Infrastructure.Model;

namespace ImageServiceWPF.VModel
{
    /// <summary>
    /// ILogsViewModel interface
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    interface ILogsViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Getter for log enteries
        /// </summary>
        ObservableCollection<MessageReceivedEventArgs> VM_LogEntries { get; }
    }
}
