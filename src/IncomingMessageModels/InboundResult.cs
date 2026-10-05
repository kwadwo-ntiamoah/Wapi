namespace Wapi.src.IncomingMessageModels
{
    /// <summary>
    /// Outcome of decoding an inbound WhatsApp webhook payload. Replaces the old
    /// (waId, displayName, BaseMessage) tuple now that a payload can also mean "we
    /// don't have a phone number for this contact yet" or "they just shared one".
    /// </summary>
    public abstract record InboundResult;

    /// <summary>Normal message from a contact we have a real wa_id for.</summary>
    public record InboundMessage(string WaId, string? DisplayName, BaseMessage Message) : InboundResult;

    /// <summary>
    /// Message from a contact WhatsApp hasn't given us a phone number for (business-scoped
    /// ID only). Caller should stash Message and call IWApi.RequestContactInfo(Bsuid, ...)
    /// instead of processing it.
    /// </summary>
    public record InboundColdContact(string Bsuid, string? Username, BaseMessage Message) : InboundResult;

    /// <summary>
    /// The contact tapped "share contact number" in response to a request_contact_info
    /// prompt. Caller should resolve its stashed InboundColdContact.Message by Bsuid and
    /// resume processing with the now-known WaId.
    /// </summary>
    public record InboundContactShared(string Bsuid, string WaId) : InboundResult;

    /// <summary>Delivery/read/etc status update — no action needed.</summary>
    public record InboundStatus(MessageStatus Status) : InboundResult;
}
