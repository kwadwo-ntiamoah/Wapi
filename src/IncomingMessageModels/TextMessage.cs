using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class TextMessage : BaseMessage
    {
        [JsonProperty("text")]
        public required Text Text { get; set; }
    }

    public class Text
    {
        [JsonProperty("body")]
        public string Body { get; set; } = string.Empty;
    }
}
