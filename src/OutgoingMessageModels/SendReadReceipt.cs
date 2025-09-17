using Newtonsoft.Json;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendReadReceipt
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; } = "whatsapp";

        [JsonProperty("status")]
        public string Status { get; set; } = "read";

        [JsonProperty("message_id")]
        public required string MessageId { get; set; }
    }
}
