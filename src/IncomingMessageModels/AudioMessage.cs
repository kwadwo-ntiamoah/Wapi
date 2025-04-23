using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class AudioMessage : BaseMessage
    {
        [JsonProperty("audio")]
        public required Media Audio { get; set; }
    }
}
