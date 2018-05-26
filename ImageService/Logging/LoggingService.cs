using Infrastructure.Enums;
using Infrastructure.Event;
using Infrastructure.Model;
using System;
using System.Collections.ObjectModel;

namespace ImageService.Logging
{
    /// <summary>
    /// LoggingService class
    /// </summary>
    /// <seealso cref="ImageService.Logging.ILoggingService" />
    public class LoggingService : ILoggingService
    {
        private ObservableCollection<MessageReceivedEventArgs> logs;
        public event EventHandler<CommandReceivedEventArgs> NewLogEntry;


        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="msg_type">The type.</param>
        public void Log(string msg, MessageTypeEnum msg_type)
        {
            //creating the new message
            MessageReceivedEventArgs new_msg = new MessageReceivedEventArgs(msg, msg_type);
            MessageReceived.Invoke(this, new_msg);
            //adding a new message
            this.logs.Add(new_msg);
            string[] args = { msg, msg_type.ToString() };
            //invoking log
            NewLogEntry?.Invoke(this, new CommandReceivedEventArgs((int)CommandEnum.LogCommand, args, null));
        }

        /// <summary>
        /// Logging service setter
        /// </summary>
        public LoggingService()
        {
            logs = new ObservableCollection<MessageReceivedEventArgs>();
        }

        /// <summary>
        /// logs
        /// </summary>
        public ObservableCollection<MessageReceivedEventArgs> Logs
        {
            get
            {
                return this.logs;
            }
        }


        /// <summary>
        /// Occurs when [message received].
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    }
}
