using Newtonsoft.Json;

namespace Wapi.src.OutgoingMessageModels
{
    // Addressed by BSUID ("recipient") rather than phone number ("to"), for contacts
    // WhatsApp hasn't given us a phone number for yet. Kept separate from
    // SendMessageBase since it doesn't share that addressing shape.
    public class SendRequestContactInfoMessage
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; } = "whatsapp";

        [JsonProperty("recipient_type")]
        public string RecipientType { get; set; } = "individual";

        [JsonProperty("recipient")]
        public required string Recipient { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = "interactive";

        [JsonProperty("interactive")]
        public required SendRequestContactInfoInteractive Interactive { get; set; }
    }

    public class SendRequestContactInfoInteractive
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "request_contact_info";

        [JsonProperty("body")]
        public required SendRequestContactInfoBody Body { get; set; }

        [JsonProperty("action")]
        public SendRequestContactInfoAction Action { get; set; } = new();
    }

    public class SendRequestContactInfoBody
    {
        [JsonProperty("text")]
        public required string Text { get; set; }
    }

    public class SendRequestContactInfoAction
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "request_contact_info";
    }
}
