using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class DocumentMessage : BaseMessage
    {
        [JsonProperty("document")]
        public required Document Document { get; set; }
    }

    public class Document : Media
    {
        [JsonProperty("filename")]
        public string Filename { get; set; } = string.Empty;
    }
}
