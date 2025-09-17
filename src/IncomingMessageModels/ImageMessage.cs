using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class ImageMessage : BaseMessage
    {
        [JsonProperty("image")]
        public required Media Image { get; set; }
    }
}
