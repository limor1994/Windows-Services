using System.Collections.ObjectModel;
using System.ComponentModel;
using ImageServiceWPF.Model;
using Infrastructure.Model;

namespace ImageServiceWPF.VModel
{
    /// <summary>
    /// LogsViewModel
    /// </summary>
    /// <seealso cref="ImageServiceWPF.VModel.ILogsViewModel" />
    class LogsViewModel : ILogsViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private ILogsModel logs_model;

        /// <summary>
        /// In case of property change, it notifies
        /// </summary>
        /// <param name="name">property name</param>
        private void NotifyPropertyChanged(string name)
        {
            //invokes
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        /// <summary>
        /// LogsViewModel Constructor
        /// </summary>
        public LogsViewModel()
        {

            //Creates a new logs model
            this.logs_model = new LogsModel();
            //delegates
            this.logs_model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        
        /// <summary>
        /// VM_LogEntries getter
        /// </summary>
        /// <value>
        /// VM_LogEntries
        /// </value>
        public ObservableCollection<MessageReceivedEventArgs> VM_LogEntries
        {
            get
            {
                return this.logs_model.LogEntries;
            }
        }
    }
}
