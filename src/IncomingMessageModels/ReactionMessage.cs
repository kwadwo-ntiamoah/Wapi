
using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class ReactionMessage : BaseMessage
    {
        [JsonProperty("reaction")]
        public required Reaction Reaction { get; set; }
    }

    public class Reaction
    {
        [JsonProperty("message_id")]
        public string MessageId { get; set; } = string.Empty;

        [JsonProperty("emoji")]
        public string Emoji { get; set; } = string.Empty;
    }
}
