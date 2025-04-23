using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class Media
    {
        [JsonProperty("mime_type")]
        public string MimeType { get; set; } = string.Empty;

        [JsonProperty("sha256")]
        public string Sha256 { get; set; } = string.Empty;

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
    }
}
