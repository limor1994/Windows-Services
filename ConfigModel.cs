using ImageService.Communication;
using ImageService.Communication.Enums;
using ImageService.Communication.Modal;
using ImageService.Modal;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ImageServiceWeb.Models
{
    public class ConfigModel
    {

        private ClientWebSingleton singleton_client;
        private Setting the_settings;


        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigModel()
        {

            //Creates new settings and client singleton instance
            the_settings = new Setting();
            singleton_client = ClientWebSingleton.getInstance;
            singleton_client.CommandReceivedEvent += settingsOnCommand;
            
            //Create new event data
            CommandReceivedEventArgs event_data = new CommandReceivedEventArgs(
                (int)CommandEnum.GetConfigCommand, null, null);
            WriteToClient(event_data);
            singleton_client.wait();

        }


        /// <summary>
        /// Command settings
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void settingsOnCommand(object sender, ClientArgs e)
        {
            //Checks if the id is get config
            if (e.CommandID == (int)CommandEnum.GetConfigCommand)
            {
                //Sets the settings, dir, sourcename and mroe
                the_settings = JsonConvert.DeserializeObject<Setting>(e.Args);
                SourceName = the_settings.SourceName;
                LogName = the_settings.LogName;
                OutPutDir = the_settings.OutPutDir;
                ThumbnailSize = the_settings.ThumbnailSize;
                HandlersArr = the_settings.ArrHandlers;

            }
            //Remove the data from hadnlers array
            else if (e.CommandID == (int)CommandEnum.RemoveHandler)
            {

                HandlersArr.Remove(e.Args);
            }
        }

        /// <summary>
        /// Writes to client the data
        /// </summary>
        /// <param name="e">event data</param>
        public void WriteToClient(CommandReceivedEventArgs e)
        {
            singleton_client.write(e);
        }



        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize:")]
        public int ThumbnailSize { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "HandlersArr:")]
        public ObservableCollection<string> HandlersArr { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName:")]
        public string SourceName { get; set; }



        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutPutDir:")]
        public string OutPutDir { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName:")]
        public string LogName { get; set; }

    }
}