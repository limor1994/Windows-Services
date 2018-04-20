using ImageService.Logging.Modal;
using System;

namespace ImageService.Logging
{
    /// <summary>Logging Interface</summary>
    public interface ILoggingService
    {
        /// <summary>Evenet in case message received</summary>
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        /// <summary>Log func</summary>
        /// <param name="message">Message</param>
        /// <param name="type">Message type by ENUM</param>
        void Log(string message, MessageTypeEnum type);
    }
}
