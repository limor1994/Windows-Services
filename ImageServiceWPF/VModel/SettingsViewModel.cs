using Prism.Commands;
using ImageServiceWPF.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Infrastructure.Enums;
using System.ComponentModel;
using Infrastructure.Event;

namespace ImageServiceWPF.VModel
{
    /// <summary>
    /// SettingsViewModel
    /// </summary>
    /// <seealso cref="ImageServiceWPF.VModel.ISettingsViewModel" />
    class SettingsViewModel : ISettingsViewModel
    {
        private ISettingsModel settings_model;
        
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// SettingsViewModel Constructor
        /// </summary>
        public SettingsViewModel()
        {

            //Creates a new model
            this.settings_model = new SettingsModel();

            //Adds changed property
            this.settings_model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            //Remove command
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
        }


        /// <summary>
        /// Checks whether it CanRemove
        /// </summary>
        /// <param name="argument">arg</param>
        /// <returns>
        /// If it can remove or not
        /// </returns>
        private bool CanRemove(object argument)
        {
            if (string.IsNullOrEmpty(this.settings_model.SelectedHandler))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Activates when removed
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnRemove(object obj)
        {            

            //Sets new arguments to selected handler
            string[] arguments = { this.settings_model.SelectedHandler };
            CommandReceivedEventArgs e_args = new CommandReceivedEventArgs((int) CommandEnum.CloseCommand, arguments, null);

            this.settings_model.Connection.Write(e_args);
        }

        /// <summary>
        /// VM_OutputDirectory Getter
        /// </summary>
        /// <value>
        /// VM_OutputDirectory
        /// </value>
        public string VM_OutputDirectory
        {
            get { return this.settings_model.OutputDirectory; }
        }


        /// <summary>
        /// RemoveCommand Setter Getter
        /// </summary>
        /// <value>
        /// RemoveCommand
        /// </value>
        public ICommand RemoveCommand
        {
            get; private set;
        }

        /// <summary>
        /// VM_LogName Getter
        /// </summary>
        /// <value>
        /// VM_LogName
        /// </value>
        public string VM_LogName
        {
            get { return this.settings_model.LogName; }
        }

        /// <summary>
        /// VM_SourceName Getter
        /// </summary>
        /// <value>
        /// VM_SourceName
        /// </value>
        public string VM_SourceName
        {
            get { return this.settings_model.SourceName; }
        }


        /// <summary>
        /// VM_ThumbnailSize getter
        /// </summary>
        /// <value>
        /// The size of the vm thumbnail.
        /// </value>
        public int VM_ThumbnailSize
        {
            get { return this.settings_model.ThumbnailSize; }
        }


        /// <summary>
        /// VM_Handlers Setter and Getter
        /// </summary>
        /// <value>
        /// VM_Handlers
        /// </value>
        public ObservableCollection<string> VM_Handlers
        {
            get { return this.settings_model.Handlers; }
            set { this.settings_model.Handlers = value; }
        }


        /// <summary>
        /// Notify in case of a prop change
        /// </summary>
        /// <param name="name"name</param>
        public void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// VM_SelectedHandler getter and setter
        /// </summary>
        /// <value>
        /// The vm selected handler.
        /// </value>
        public string VM_SelectedHandler
        {
            get { return this.settings_model.SelectedHandler; }
            set
            {

                //Sets it using command
                this.settings_model.SelectedHandler = value;
                var cmd = this.RemoveCommand as DelegateCommand<object>;
                cmd.RaiseCanExecuteChanged();
            }
        }


    }
}
