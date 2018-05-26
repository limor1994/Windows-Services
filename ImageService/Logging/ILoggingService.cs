using Infrastructure.Event;
using Infrastructure.Model;
using System;
using System.Collections.ObjectModel;

namespace ImageService.Logging
{
    /// <summary>
    /// ILoggingService interface
    /// </summary>
    public interface ILoggingService
    {

        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<CommandReceivedEventArgs> NewLogEntry;

        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="msg">message</param>
        /// <param name="msg_type">message type</param>
        void Log(string msg, MessageTypeEnum msg_type);


        /// <summary>
        /// Logs getter
        /// </summary>
        ObservableCollection<MessageReceivedEventArgs> Logs { get; }


    }
}
