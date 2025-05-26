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

        public ErrorOr<(string?, BaseMessage)> DecodeInboundMessage(string payload)
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
                    
                }

                // contains actual content of message
                if (change?.Value.Messages != null)
                {
                    var displayName = change?.Value.Contacts.FirstOrDefault()?.Profile.Name;
                    var message = change?.Value.Messages.FirstOrDefault();

                    return (displayName, message!);
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
    }
}
