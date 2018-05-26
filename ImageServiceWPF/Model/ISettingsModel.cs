using System.ComponentModel;
using ImageServiceWPF.Client;
using System.Collections.ObjectModel;


namespace ImageServiceWPF.Model
{
    /// <summary>
    /// ISettingsModel interface
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    interface ISettingsModel : INotifyPropertyChanged
    {


        /// <summary>
        /// Getter and Setter for OutputDirectory
        /// </summary>
        string OutputDirectory { get; set; }

        /// <summary>
        /// Getter and Setter for Handlers
        /// </summary>
        ObservableCollection<string> Handlers { get; set; }

        /// <summary>
        /// Getter and Setter for thumbnail size
        /// </summary>
        int ThumbnailSize { get; set; }

        /// <summary>
        /// Getter and Setter for SourceName
        /// </summary>
        string SourceName { get; set; }

        /// <summary>
        /// Getter and Setter for SelectedHandler
        /// </summary>
        string SelectedHandler { get; set; }

        /// <summary>
        /// Getter and Setter for Connection
        /// </summary>
        IClientConnection Connection { get; }


        /// <summary>
        /// Getter and Setter for log name
        /// </summary>
        string LogName { get; set; }

    }
}
