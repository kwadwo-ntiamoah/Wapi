using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendTextMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "text";

        [JsonProperty("text")]
        public required SendText Text { get; set; }
    }

    public class SendText
    {
        [JsonProperty("preview_url")]
        public bool PreviewUrl { get; set; } = false;

        [JsonProperty("body")]
        public string Body { get; set; } = string.Empty;
    }
}
