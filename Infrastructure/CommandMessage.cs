using Newtonsoft.Json.Linq;

namespace Infrastructure
{
    /// <summary>
    /// CommandMessage
    /// </summary>
    public class CommandMessage
    {


        /// <summary>
        /// Parse the given JSON
        /// </summary>
        /// <param name="str">the string to parse</param>
        /// <returns></returns>
        public static CommandMessage ParseJSON(string str)
        {

            //Creates a new message
            CommandMessage message = new CommandMessage();
            JObject commandObject = JObject.Parse(str);
            //Sets command ID and arguments
            message.CommandID = (int)commandObject["CommandID"];
            JObject array = (JObject)commandObject["CommandArgs"];
            message.CommandArgs = array;
            return message;
        }

        /// <summary>
        /// command args setter and getter
        /// </summary>
        /// <value>
        /// command args 
        /// </value>
        public JObject CommandArgs { get; set; }

        /// <summary>
        /// changes to JSON
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            //creates a new cmd object
            JObject command_object = new JObject();
            command_object["CommandID"] = CommandID;
            JObject arguemnts = new JObject(CommandArgs);
            command_object["CommandArgs"] = arguemnts;
            return command_object.ToString();
        }

        /// <summary>
        /// command identifier setter and getter
        /// </summary>
        /// <value>
        /// command identifier
        /// </value>
        public int CommandID { get; set; }



    }
}
