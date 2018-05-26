using System;

namespace Infrastructure.Model
{
    /// <summary>
    /// MessageReceivedEventArgs
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// MessageReceivedEventArgs Constructor
        /// </summary>
        /// <param name="msg">message</param>
        /// <param name="type_enum">enum</param>
        public MessageReceivedEventArgs(string msg, MessageTypeEnum type_enum)
        {
            Status = type_enum;
            Message = msg;
        }

        /// <summary>
        /// Message setter and getter
        /// </summary>
        /// <value>
        /// message
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Status setter and getter
        /// </summary>
        /// <value>
        /// status
        /// </value>
        public MessageTypeEnum Status { get; set; }

    }
}
