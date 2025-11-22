using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.IncomingMessageModels
{
    public class MessageStatus: BaseMessage
    {
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;  
    }
}
