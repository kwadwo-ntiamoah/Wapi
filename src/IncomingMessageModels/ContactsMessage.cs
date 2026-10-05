using Newtonsoft.Json;

namespace Wapi.src.IncomingMessageModels
{
    // Confirmed against a real sandbox payload from tapping "share contact number":
    // message.type == "contacts", each entry under contacts[] carries a "vcard", a
    // "name", "phones[]" (phone/wa_id/type), and "origin" as a plain string
    // ("contact_request") - NOT an object, and nested per-contact, not on the message.
    // Note WhatsApp also fills in the outer envelope's message.from/contacts[].wa_id
    // with the real number on this reply, but we still route it through
    // InboundContactShared rather than the normal InboundMessage path, since this
    // message is our own request_contact_info round-trip, not a message the bot
    // should process as user input.
    public class ContactsMessage : BaseMessage
    {
        [JsonProperty("contacts")]
        public List<SharedContact> Contacts { get; set; } = [];
    }

    public class SharedContact
    {
        [JsonProperty("phones")]
        public List<SharedContactPhone> Phones { get; set; } = [];

        [JsonProperty("origin")]
        public string Origin { get; set; } = string.Empty;
    }

    public class SharedContactPhone
    {
        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonProperty("wa_id")]
        public string WaId { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}
