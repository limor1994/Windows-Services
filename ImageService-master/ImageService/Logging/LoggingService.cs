using ImageService.Logging.Modal;
using System;

namespace ImageService.Logging
{
    /// <summary>Main Log Class</summary>
    public class LoggingService : ILoggingService
    {

        /// <summary>Event Handler for received messages </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        /// <summary>Log func</summary>
        /// <param name="message">Message</param>
        /// <param name="type">Message type by ENUM</param>
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));

        }
    }

}
