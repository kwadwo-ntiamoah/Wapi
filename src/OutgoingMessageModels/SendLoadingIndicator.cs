using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendLoadingIndicator
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; } = "whatsapp";

        [JsonProperty("status")]
        public string Status { get; set; } = "read";

        [JsonProperty("message_id")]
        public required string MessageId { get; set; }

        [JsonProperty("typing_indicator")]
        public TypingIndicator? TypingIndicator { get; set; } = new() { Type = "text" };
    }

    public class TypingIndicator
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "text";
    }
}
