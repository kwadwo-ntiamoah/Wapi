using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class StickerMessage : BaseMessage
    {
        [JsonProperty("sticker")]
        public required Sticker Sticker { get; set; }
    }

    public class Sticker : Media
    {
        [JsonProperty("animated")]
        public bool Animated { get; set; }
    }
}
