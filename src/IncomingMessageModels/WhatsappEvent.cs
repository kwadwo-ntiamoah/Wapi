using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    public class WhatsAppEvent
    {
        [JsonProperty("object")]
        public string Object { get; set; } = string.Empty;

        [JsonProperty("entry")]
        public List<Entry> Entry { get; set; } = [];
    }

    public class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("changes")]
        public List<Change> Changes { get; set; } = [];
    }

    public class Change
    {
        [JsonProperty("field")]
        public string Field { get; set; } = string.Empty;

        [JsonProperty("value")]
        public required Value Value { get; set; }
    }

    public class Value
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; } = string.Empty;

        [JsonProperty("metadata")]
        public required Metadata Metadata { get; set; }

        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; } = [];

        [JsonProperty("messages")]
        public List<BaseMessage> Messages { get; set; } = [];

        [JsonProperty("statuses")]
        public List<Status> Statuses { get; set; } = [];
    }

    public class Metadata
    {
        [JsonProperty("display_phone_number")]
        public string DisplayPhoneNumber { get; set; } = string.Empty;

        [JsonProperty("phone_number_id")]
        public string PhoneNumberId { get; set; } = string.Empty;
    }

    public class Contact
    {
        [JsonProperty("profile")]
        public required Profile Profile { get; set; }

        [JsonProperty("wa_id")]
        public string WaId { get; set; } = string.Empty;
    }

    public class Profile
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class Status
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("status")]
        public string StatusType { get; set; } = string.Empty;

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("recipient_id")]
        public string RecipientId { get; set; } = string.Empty;

        [JsonProperty("conversation")]
        public required Conversation Conversation { get; set; }

        [JsonProperty("pricing")]
        public required Pricing Pricing { get; set; }
    }

    public class Conversation
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("expiration_timestamp")]
        public long ExpirationTimestamp { get; set; }

        [JsonProperty("origin")]
        public required Origin Origin { get; set; }
    }

    public class Origin
    {
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }

    public class Pricing
    {
        [JsonProperty("billable")]
        public bool Billable { get; set; }

        [JsonProperty("pricing_model")]
        public string PricingModel { get; set; } = string.Empty;

        [JsonProperty("category")]
        public string Category { get; set; } = string.Empty;
    }

}
