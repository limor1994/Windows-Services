using System;


namespace ImageService.Logging.Modal
{
    /// <summary>Event in case a message received</summary>
    public class MessageRecievedEventArgs : EventArgs
    {
		//Class members
		
        #region members
        private MessageTypeEnum m_status;
        private string m_message;
        #endregion
        #region Properties
        public MessageTypeEnum Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }
        #endregion
        /// <summary>Arguments of received message</summary>
        /// <param name="status">Current status of the message</param>
        /// <param name="message">The message itself</param>
        public MessageRecievedEventArgs(MessageTypeEnum status, string message)
        {
            this.m_status = status;
            this.m_message = message;
        }
    }
}
