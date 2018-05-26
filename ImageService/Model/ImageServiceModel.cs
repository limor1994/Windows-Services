using ImageService.Logging;
using System.Text;
using System.Collections.ObjectModel;
using System.Configuration;
using Infrastructure.Model;
using ImageService.Server;
using Infrastructure;
using Infrastructure.Enums;
using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImageService.Model
{
    /// <summary>
    /// tImageServiceModel class
    /// </summary>
    /// <seealso cref="ImageService.Model.IImageServiceModel" />
    public class ImageServiceModel : IImageServiceModel
    {
        private string service_dir = null;
        private int size_thumb;


        /// <summary>
        /// Updates the entries.
        /// </summary>
        /// <param name="img_server">The server.</param>
        /// <param name="operation_result">if set to <c>true</c> [result].</param>
        /// <returns></returns>
        public string UpdateEntries(ImageServer img_server, out bool operation_result)
        {
            try
            {
                //sets the logging service
                ILoggingService logging_service = img_server.Logging;
                CommandMessage msg = new CommandMessage();
                msg.CommandID = (int)CommandEnum.LogCommand;
                JObject job_object = new JObject();

                //the logs
                ObservableCollection<MessageReceivedEventArgs> logs = logging_service.Logs;
                var json_var = JsonConvert.SerializeObject(logs);

                //sets log entries
                job_object["LogEntries"] = json_var;
                msg.CommandArgs = job_object;
                operation_result = true;
                return msg.ToJSON();
            }
            catch (Exception e)
            {
                operation_result = false;
                return e.Message;
            }
        }



        /// <summary>
        /// Adds file
        /// </summary>
        /// <param name="dir_path">directory path</param>
        /// <param name="operation_result">the operation's result</param>
        /// <returns></returns>
        public string AddFile(string dir_path, out bool operation_result)
        {
            //First checks if the file exists
            if (File.Exists(dir_path))
            {
                try
                {
                    //In case it doesn't exist, create it
                    if (!Directory.Exists(service_dir))
                    {
                        DirectoryInfo newDir = Directory.CreateDirectory(service_dir);
                        //sets attributes
                        newDir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    }

                    //get's the thumbnail's path and creating the directory
                    string thumnbail_path = System.IO.Path.Combine(service_dir.ToString(), "Thumbnails");
                    Directory.CreateDirectory(thumnbail_path);
                    //Creation date and time
                    DateTime time = File.GetCreationTime(dir_path);
                    //get year and month
                    string time_year = time.Year.ToString();
                    string time_month = time.Month.ToString();
                    //gets the year and month paths
                    string year_path = System.IO.Path.Combine(service_dir.ToString(), time_year);
                    string month_path = System.IO.Path.Combine(year_path, time_month);
                    //creates a new directory
                    Directory.CreateDirectory(month_path);

                    //Sets thumbnails directories
                    string year_thumbnail = System.IO.Path.Combine(thumnbail_path, time_year);
                    string month_thumbnail = System.IO.Path.Combine(year_thumbnail, time_month);
                    Directory.CreateDirectory(month_thumbnail);

                    //sets full image path
                    string img_main_path = System.IO.Path.Combine(month_path, Path.GetFileName(dir_path));

                    //checks if already exists
                    if (!File.Exists(img_main_path))
                    {
                        //moves the file
                        File.Move(dir_path, img_main_path);
                    }

                    //Saves the thumbnail
                    string thumbnail_main_path = System.IO.Path.Combine(month_thumbnail, Path.GetFileName(dir_path));
                    if (!File.Exists(thumbnail_main_path))
                    {
                        Image img = Image.FromFile(img_main_path);
                        Image thumb = img.GetThumbnailImage(this.size_thumb, this.size_thumb, () => false, IntPtr.Zero);
                        thumb.Save(thumbnail_main_path);
                    }
                    
                    //sets the operation result
                    operation_result = true;
                    return "Image: " + img_main_path + " was moved";
                } catch(Exception e)
                {
                    operation_result = false;
                    return e.Message;
                }
            } else
            {
                //In case there isn't a file
                operation_result = false;
                return "File doesn't exist";
            }
        }



        /// <summary>
        /// ImageServiceModel Constructor
        /// </summary>
        /// <param name="dir">output directory</param>
        /// <param name="size">thumbnail size</param>
        public ImageServiceModel(string dir, int size)
        {
            this.service_dir = dir;
            this.size_thumb = size;
        }



        /// <summary>
        /// Builds a handler removed
        /// </summary>
        /// <param name="handlerRemoved">The handler</param>
        /// <param name="operation_result">operation result</param>
        /// <returns></returns>
        public string BuildHandlerRemovedMessage(string handlerRemoved, out bool operation_result)
        {
            try
            {
                StringBuilder string_builder = new StringBuilder();
                string[] handlersString = ConfigurationManager.AppSettings.Get("Handler").Split(';');
                foreach (string handlerString in handlersString)
                {
                    if (string.Compare(handlerRemoved, handlerString) != 0)
                    {
                        string_builder.Append(handlerString);
                        string_builder.Append(";");
                    }
                }
                string newHandlers = (string_builder.ToString()).TrimEnd(';');
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //configuration.AppSettings.Settings.Remove("Handler");
                //configuration.AppSettings.Settings.Add("Handler", newHandlers);
                configuration.AppSettings.Settings["Handler"].Value = newHandlers;
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                //TO DO: fix erasing from app config

                //creates a new message
                CommandMessage msg = new CommandMessage();
                msg.CommandID = (int)CommandEnum.CloseCommand;
                JObject job_object = new JObject();
                job_object["HandlerRemoved"] = handlerRemoved;
                msg.CommandArgs = job_object;
                //returns the msg
                operation_result = true;
                return msg.ToJSON();  
            }
            catch (Exception e)
            {
                operation_result = false;
                return e.Message;
            }
        }

        /// <summary>
        /// Builds the config
        /// </summary>
        /// <param name="operation_result">the operation's result</param>
        /// <returns></returns>
        public string BuildConfig(out bool operation_result)
        {
            try
            {
                //creates a new command message
                CommandMessage command_message = new CommandMessage();
                command_message.CommandID = (int)CommandEnum.GetConfigCommand;
                JObject current_job_object = new JObject();
                current_job_object["OutputDirectory"] = ConfigurationManager.AppSettings["OutputDir"];
                current_job_object["SourceName"] = ConfigurationManager.AppSettings["SourceName"];
                current_job_object["LogName"] = ConfigurationManager.AppSettings["LogName"];
                current_job_object["ThumbnailSize"] = ConfigurationManager.AppSettings["ThumbnailSize"];

                //Job array
                JArray j_array = new JArray();
                string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');
                j_array = JArray.FromObject(handlers);
                current_job_object["Handlers"] = j_array;
                command_message.CommandArgs = current_job_object;
                string json_config = command_message.ToJSON();
                operation_result = true;
                return json_config;
            }
            catch (Exception e)
            {
                operation_result = false;
                return e.Message;
            }
        }



    }
}
