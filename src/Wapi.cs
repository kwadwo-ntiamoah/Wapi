using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wapi.src.Extensions;
using Wapi.src.Http;
using Wapi.src.IncomingMessageModels;
using Wapi.src.MessageResponse;
using ErrorOr;
using Wapi.src.OutgoingMessageModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Wapi.src
{
    public class WApi(WhatsappClient client, IOptions<WhatsappConfig> config) : IWApi
    {
        private readonly WhatsappClient _client = client;
        private readonly WhatsappConfig _whatsappConfig = config.Value;

        public ErrorOr<string> ValidateInboundMessage(IQueryCollection queries)
        {
            try
            {
                var hubMode = queries["hub.mode"].ToString();
                var hubVerificationToken = queries["hub.verify_token"].ToString();
                var hubChallenge = queries["hub.challenge"]!.ToString();

                var verificationToken = _whatsappConfig.VerificationToken;
                var isValidRequest = hubMode == "subscribe" && hubVerificationToken == verificationToken;

                if (isValidRequest) return hubChallenge!;
                else return new Error[] { Error.Unauthorized(description: "Invalid request. Request will be rejected") };
            }
            catch (Exception)
            {
                return new Error[] { Error.Unauthorized(description: "Invalid request. Request will be rejected") };
            }
        }

        public ErrorOr<InboundResult> DecodeInboundMessage(string payload)
        {
            try
            {
                // dynamically converts whatsapp message to a type
                var settings = new JsonSerializerSettings
                {
                    Converters = [new BaseMessageConverter()],
                    TypeNameHandling = TypeNameHandling.None
                };

                var inboundMessage = JsonConvert.DeserializeObject<WhatsAppEvent>(payload, settings);

                if (inboundMessage?.Entry == null) return new Error[] { Error.Failure(description: "An error occurred decoding inbound message") };

                var entry = inboundMessage.Entry.First();
                var change = entry.Changes.FirstOrDefault();

                // message statuses like delivered, read, not delivered etc
                if (change?.Value.Statuses.Count > 0)
                {
                    var status = change?.Value.Statuses.FirstOrDefault();
                    var messageStatus = new MessageStatus
                    {
                        Status = status?.StatusType ?? "delivered"
                    };

                    return new InboundStatus(messageStatus);
                }

                // contains actual content of message
                if (change?.Value.Messages != null)
                {
                    var message = change.Value.Messages.FirstOrDefault();
                    var contact = change.Value.Contacts.FirstOrDefault();

                    if (message == null) return new Error[] { Error.Validation(description: "Invalid message received") };

                    // Reply to our request_contact_info prompt - confirmed shape, see ContactsMessage.
                    if (message is ContactsMessage contactsMessage)
                    {
                        var sharedWaId = contactsMessage.Contacts
                            .FirstOrDefault(c => c.Origin == "contact_request")?.Phones.FirstOrDefault()?.WaId;

                        if (!string.IsNullOrEmpty(message.FromUserId) && !string.IsNullOrEmpty(sharedWaId))
                        {
                            return new InboundContactShared(message.FromUserId, sharedWaId);
                        }
                    }

                    // Cold contact: WhatsApp hasn't given us a phone number for them yet
                    // (business-scoped ID only - no wa_id/from on this payload).
                    if (string.IsNullOrEmpty(message.From) && !string.IsNullOrEmpty(message.FromUserId))
                    {
                        return new InboundColdContact(message.FromUserId, contact?.Profile.Username, message);
                    }

                    var waId = contact?.WaId ?? message.From;
                    var displayName = contact?.Profile.Name;
                    return new InboundMessage(waId, displayName, message);
                }

                return new Error[] { Error.Validation(description: "Invalid message received") };
            }
            catch (Exception ex)
            {
                return new Error[] { Error.Failure(description: ex.Message) };
            }
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendAudio message)
        {
            var payload = new SendAudioMessage
            {
                To = recipient,
                Audio = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendContact message)
        {
            var payload = new SendContactMessage
            {
                To = recipient,
                Contacts = [message]
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, List<SendContact> message)
        {
            var payload = new SendContactMessage
            {
                To = recipient,
                Contacts = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendCTAInteractive message)
        {
            var payload = new SendCTAMessage
            {
                To = recipient,
                Interactive = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendDocument message)
        {
            var payload = new SendDocumentMessage
            {
                To = recipient,
                Document = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendFlowInteractive message)
        {
            var payload = new SendFlowMessage
            {
                To = recipient,
                Interactive = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendImage message)
        {
            var payload = new SendImageMessage
            {
                To = recipient,
                Image = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendListInteractive message)
        {
            var payload = new SendListMessage
            {
                To = recipient,
                Interactive = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendLocation message)
        {
            var payload = new SendLocationMessage
            {
                To = recipient,
                Location = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendQuickReply message)
        {
            var payload = new SendQuickReplyMessage
            {
                To = recipient,
                Interactive = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendReaction message)
        {
            var payload = new SendReactionMessage
            {
                To = recipient,
                Reaction = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendReadReceipt message)
        {
            var response = await _client.SendAsync(message);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendSticker message)
        {
            var payload = new SendStickerMessage
            {
                To = recipient,
                Sticker = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendText message)
        {
            var payload = new SendTextMessage
            {
                To = recipient,
                Text = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> SendMessage(string recipient, SendVideo message)
        {
            var payload = new SendVideoMessage
            {
                To = recipient,
                Video = message
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<bool>> ShowLoadingIndicator(string messageId)
        {
            var payload = new SendLoadingIndicator { MessageId = messageId };
            var response = await _client.SendAsync(payload);

            return !response.IsError;
        }

        public async Task<ErrorOr<OutBoundMessageResponse>> RequestContactInfo(string bsuid, string bodyText)
        {
            var payload = new SendRequestContactInfoMessage
            {
                Recipient = bsuid,
                Interactive = new SendRequestContactInfoInteractive
                {
                    Body = new SendRequestContactInfoBody { Text = bodyText }
                }
            };

            var response = await _client.SendAsync(payload);
            return response.IsError ? response : response.Value;
        }

        public async Task<ErrorOr<(string, string)>> GetMedia(string mediaId)
        {
            try
            {
                var mediaUrl = await _client.GetMediaUrlAsync(mediaId);
                if (mediaUrl.IsError) return new Error[] { Error.Failure(description: "Failed to get media URL") };

                var mediaBase64 = await _client.GetMediaBase64String(mediaUrl.Value);
                if (mediaBase64.IsError) return new Error[] { Error.Failure(description: "Failed to get media Base64 string") };

                return (mediaUrl.Value, mediaBase64.Value);

            } catch (Exception ex)
            {
                return new Error[] { Error.Failure(description: ex.Message) };
            }
        }
    }
}
