using Newtonsoft.Json;

namespace Wapi.src.OutgoingMessageModels
{
    public class SendMessageBase
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; } = "whatsapp";

        [JsonProperty("recipient_type")]
        public string RecipientType { get; set; } = "individual";

        [JsonProperty("to")]
        public string To { get; set; } 
    }
}
