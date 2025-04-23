using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendReactionMessage: SendMessageBase
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "reaction";

        [JsonProperty("reaction")]
        public SendReaction? Reaction { get; set; }
    }

    public class SendReaction
    {
        [JsonProperty("message_id")]
        public required string MessageId { get; set; }

        [JsonProperty("emoji")]
        public required string Emoji { get; set; }
    }
}
