using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.IncomingMessageModels
{
    public class InteractiveQuickReplyMessage : InteractiveBaseMessage
    {
        [JsonProperty("interactive")]
        public InteractiveQuickReply? Interactive { get; set; }
    }

    public class InteractiveQuickReply
    {

        [JsonProperty("button_reply")]
        public ButtonReply? ButtonReply { get; set; }
    }

    public class ButtonReply
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
    }
}
